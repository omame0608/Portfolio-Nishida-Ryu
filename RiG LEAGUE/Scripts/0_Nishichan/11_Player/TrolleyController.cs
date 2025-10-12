using Cysharp.Threading.Tasks;
using DG.Tweening;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using VContainer;

/// <summary>
/// プレイヤーであるTrolleyを操作するクラス
/// </summary>
/// <author>西田琉</author>
public class TrolleyController : MonoBehaviour
{
    //プロパティ
    public bool CanJump { get; set; } = true; //ジャンプ入力の受付の可否
    public bool CanAnswer { get; set; } = false; //回答入力の受付の可否
    public int AnsweredNumber { get; private set; } //回答した番号、0で未回答

    //各プレイヤーに関するデータ
    public int Score { get; private set; } //スコア
    public int Ranking { get; set; } //順位

    //キャッシュ
    private PlayerInput _playerInput; //入力受付コンポーネント
    private ITrolleySystemFacade _trolleySystemFacade; //TrolleyのView操作用

    //参照
    private InputAction _jump;    //ジャンプアクション
    private InputAction _select1; //選択肢1アクション
    private InputAction _select2; //選択肢2アクション
    private InputAction _select3; //選択肢3アクション
    private InputAction _aori;    //煽りアクション
    [Inject] private GameViewController _gameViewController; //ゲームView操作用

    //トロッコ生成座標指定用
    private static readonly Vector3 _LEFT_POSITION = new Vector3(-5.25f, -1.37f, -4.43f); //一番左のトロッコのワールド座標
    private static readonly Vector3 _DELTA_POSITION = new Vector3(1.5f, 0f, 0f);          //左のトロッコからの差分

    //トロッコ色指定用配列
    //各プレイヤーに割り当てるカラーが格納されてる
    [SerializeField] private Material[] _materialArray = new Material[8];

    //PlayerPanel制御用
    [SerializeField] private GameObject _playerPanelPrefab; //プレイヤー情報パネルプレハブ
    public PlayerPanel PlayerPanel { get; private set; } //パネル操作用

    //AnswerCheckPanel制御用
    [SerializeField] private GameObject _answerCheckPanelPrefab; //回答確認パネルプレハブ
    private AnswerCheckPanel _answerCheckPanel; //パネル操作用

    //UniTaskフラッグ
    public UniTaskCompletionSource<bool> AnswerDisplayFlag { get; private set; } //回答表示の終了を待機する

    //Audio操作用
    [Inject] private SEManager _seManager;

    //順位表示用プレハブ　書き直したい
    [SerializeField] private GameObject _rank1;
    [SerializeField] private GameObject _rank2;
    [SerializeField] private GameObject _rank3;
    [SerializeField] private GameObject _rank4;
    [SerializeField] private GameObject _rank5;
    [SerializeField] private GameObject _rank6;
    [SerializeField] private GameObject _rank7;
    [SerializeField] private GameObject _rank8;



    void Awake()
    {
        //各種キャッシュの取得
        _playerInput = GetComponent<PlayerInput>();
        _trolleySystemFacade = GetComponentInChildren<ITrolleySystemFacade>();

        //各種アクションの取得
        _playerInput.currentActionMap.Enable();
        _jump = _playerInput.currentActionMap.FindAction("Jump");
        _select1 = _playerInput.currentActionMap.FindAction("Select1");
        _select2 = _playerInput.currentActionMap.FindAction("Select2");
        _select3 = _playerInput.currentActionMap.FindAction("Select3");
        _aori = _playerInput.currentActionMap.FindAction("Aori");
    }


    void Start()
    {
        //自身に割り当てられたインデックスを取得
        int index = _playerInput.user.index;

        //トロッコの初期位置設定
        transform.position = _LEFT_POSITION + index * _DELTA_POSITION;

        //トロッコのカラー設定
        GameObject view = transform.GetChild(0).gameObject; //トロッコの見た目オブジェクト本体

        //胴体
        Material[] tmp = view.transform.GetChild(13).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
        tmp[0] = _materialArray[index];
        view.transform.GetChild(13).gameObject.GetComponent<SkinnedMeshRenderer>().materials = tmp;

        //タイヤ4つ
        for (int i = 1; i <= 4; i++)
        {
            tmp = view.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
            tmp[1] = _materialArray[index];
            view.transform.GetChild(i).gameObject.GetComponent<SkinnedMeshRenderer>().materials = tmp;
        }

        //人オブジェクト
        GameObject human = view.transform.GetChild(0).GetChild(0).GetChild(2).gameObject; //人モデル本体
        //服
        tmp = human.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
        tmp[1] = _materialArray[index];
        human.transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().materials = tmp;
        //帽子
        tmp = human.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().materials;
        tmp[5] = _materialArray[index];
        human.transform.GetChild(2).gameObject.GetComponent<SkinnedMeshRenderer>().materials = tmp;

        //各プレイヤー用のUIパネルを表示
        var panel = Instantiate(_playerPanelPrefab, GameObject.Find("PlayerPanels").transform);
        panel.GetComponent<RectTransform>().localPosition += new Vector3(index * 100f, 0f, 0f);
        PlayerPanel = panel.GetComponent<PlayerPanel>();

        //各プレイヤー用の回答表示用パネルを生成（最初は非表示）
        panel = Instantiate(_answerCheckPanelPrefab, GameObject.Find("AnswerCheckPanels").transform);
        panel.GetComponent<RectTransform>().localPosition += new Vector3(index * 100f, 0f, 0f);
        _answerCheckPanel = panel.GetComponent<AnswerCheckPanel>();
    }


    //入力応答　イベント駆動にするかも
    void Update()
    {
        //ジャンプ入力受付
        if (CanJump && _jump.WasPressedThisFrame())
        {
            _trolleySystemFacade.JumpTrolley();
        }

        //回答入力受付
        if (CanAnswer)
        {
            //選択肢1を選択
            if (_select1.WasPressedThisFrame())
            {
                TransitionState(PlayerPanel.State.Answered);
                _answerCheckPanel.SetAnswerNumber(1);
                AnsweredNumber = 1;
                _seManager.PlaySE(SEManager.SE.SelectNumber);
            }

            //選択肢2を選択
            if (_select2.WasPressedThisFrame())
            {
                TransitionState(PlayerPanel.State.Answered);
                _answerCheckPanel.SetAnswerNumber(2);
                AnsweredNumber = 2;
                _seManager.PlaySE(SEManager.SE.SelectNumber);
            }

            //選択肢3を選択
            if (_select3.WasPressedThisFrame())
            {
                TransitionState(PlayerPanel.State.Answered);
                _answerCheckPanel.SetAnswerNumber(3);
                AnsweredNumber = 3;
                _seManager.PlaySE(SEManager.SE.SelectNumber);
            }
        }

        //煽り入力受付
        if (_aori.WasPerformedThisFrame())
        {
            _seManager.PlaySE(SEManager.SE.Aori);
        }
    }


    /// <summary>
    /// 回答した番号を初期化し未回答状態に
    /// </summary>
    public void ResetAnsweredNumber()
    {
        AnsweredNumber = 0;
    }


    /// <summary>
    /// プレイヤーの状態を更新
    /// </summary>
    /// <param name="state">更新後の状態</param>
    public void TransitionState(PlayerPanel.State state)
    {
        PlayerPanel.TransitionState(state);
    }


    /// <summary>
    /// プレイヤーの回答を表示
    /// </summary>
    public async void DisplayOwnAnswer()
    {
        var rect = _answerCheckPanel.gameObject.GetComponent<RectTransform>();

        //フラグが立つまで表示しておくs
        _answerCheckPanel.gameObject.SetActive(true);
        AnswerDisplayFlag = new UniTaskCompletionSource<bool>();
        await rect.DOScale(new Vector2(1.1f, 1.1f), 0.5f).SetEase(Ease.OutElastic);
        await AnswerDisplayFlag.Task;
        AnswerDisplayFlag = null;

        //非表示にする
        await rect.DOScale(new Vector2(1f, 1f), 0f);
        _answerCheckPanel.gameObject.SetActive(false);
    }


    /// <summary>
    /// スコア加算メソッド
    /// </summary>
    /// <param name="add">加算値</param>
    public void AddScore(int add)
    {
        if (add <= 0) Debug.LogError("スコアには正の整数を加算してください");
        Score += add;
        if (Score > 999) Score = 999;

        //UIを更新
        PlayerPanel.UpdateScoreText(Score);
    }


    /// <summary>
    /// スコア減算メソッド
    /// </summary>
    /// <param name="sub">減算値</param>
    public void SubstructScore(int sub)
    {
        if (sub <= 0) Debug.LogError("スコアには正の整数を減算してください");
        Score -= sub;
        if (Score < 0) Score = 0;

        //UIを更新
        PlayerPanel.UpdateScoreText(Score);
    }


    /// <summary>
    /// トロッコがコインを取得する
    /// </summary>
    public void GetCoin()
    {
        //+1点
        AddScore(1);
        //Audio
        _seManager.PlaySE(SEManager.SE.Coin);
    }


    /// <summary>
    /// トロッコがダメージを受ける
    /// </summary>
    public void HitTrolley()
    {
        //ダメージアニメーション
        _trolleySystemFacade.DamageTrolley();
        //-1点
        SubstructScore(1);
        //Audio
        _seManager.PlaySE(SEManager.SE.Damage);
    }


    /// <summary>
    /// トロッコの動く・止まるを指定
    /// </summary>
    /// <param name="on">trueなら動かす、falseなら止める</param>
    public void SwitchTrolleyMove(bool on)
    {
        if (on) _trolleySystemFacade.MoveTrolley();
        else _trolleySystemFacade.StopTrolley();
    }


    /// <summary>
    /// 順位を表示する　書き直したい
    /// </summary>
    /// <param name="rank">順位</param>
    public async void DisplayRank(int rank)
    {
        GameObject prefab = null;
        switch (rank)
        {
            case 1: 
                prefab = _rank1;
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                _seManager.PlaySE(SEManager.SE.Rank1);
                break;
            case 2: 
                prefab = _rank2;
                await UniTask.Delay(TimeSpan.FromSeconds(2f));
                _seManager.PlaySE(SEManager.SE.Rank2);
                break;
            case 3: 
                prefab = _rank3;
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                _seManager.PlaySE(SEManager.SE.Rank3);
                break;
            case 4: prefab = _rank4; break;
            case 5: prefab = _rank5; break;
            case 6: prefab = _rank6; break;
            case 7: prefab = _rank7; break;
            case 8: prefab = _rank8; break;
        }

        GameObject panel = Instantiate(prefab, GameObject.Find("RankingPanels").transform);
        panel.GetComponent<RectTransform>().localPosition += new Vector3(_playerInput.user.index * 100f, 0f, 0f);
    }
}
