using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;

/// <summary>
/// ゲームの進行に必要なViewを制御するクラス
/// </summary>
/// <author>西田琉</author>
public class GameViewController : MonoBehaviour
{
    //キャンバス
    [SerializeField] private GameObject _canvasObject; //UIを生成する際の親になるオブジェクト

    //制御するView一覧
    [SerializeField] private GameObject _quizNumberPanel; //「第〇問」とか書いてるパネル
    [SerializeField] private GameObject _quizPanel; //クイズが書いてあるパネル
    [SerializeField] private GameObject _timeUpPanel; //カウント終了時のパネル
    [SerializeField] private GameObject _answerPanel; //解答と解説が書いてあるパネル
    [SerializeField] private GameObject _thanksPanel; //エンディングパネル

    //演出用3Dオブジェクト
    [SerializeField] private GameObject _bird;
    [SerializeField] private GameObject _dragon;

    //UniTaskフラッグ
    public UniTaskCompletionSource<bool> QuizFlag {  get; private set; } //クイズのタイムアップを待機する

    //Audio
    [Inject] private SEManager _seManager;


    /// <summary>
    /// 「第〇問」とか書いてるパネルを表示する
    /// 一定時間経過まで
    /// </summary>
    /// <param name="turn">表示するターン番号</param>
    public async UniTask DisplayQuizNumberPanel(int turn)
    {
        //対象のパネルを生成（初期状態で非Active）
        GameObject panel = Instantiate(_quizNumberPanel, _canvasObject.transform);

        //パネルの設定
        panel.GetComponentInChildren<TextMeshProUGUI>().text = $"第{turn}問";

        //パネルを一定時間表示させてから非表示に戻す
        panel.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        panel.SetActive(false);

        //パネルをしまう
        Destroy(panel);
    }


    /// <summary>
    /// 問題が書かれたパネルを表示する
    /// フラグが立つまで
    /// </summary>
    /// <param name="quiz">表示する問題インスタンス</param>
    public async void DisplayQuizPanel(IQuiz quiz)
    {
        //対象のパネルを生成（初期状態で非Active）
        GameObject panel = Instantiate(_quizPanel, _canvasObject.transform);

        //パネルの設定
        panel.GetComponentInChildren<Text>().text = quiz.QuizSentence;
        panel.transform.GetChild(1).GetComponent<Text>().text = quiz.Select1;
        panel.transform.GetChild(2).GetComponent<Text>().text = quiz.Select2;
        panel.transform.GetChild(3).GetComponent<Text>().text = quiz.Select3;

        //パネルをクイズ終了まで表示させてから非表示に戻す
        panel.SetActive(true);
        await panel.GetComponent<RectTransform>().DOAnchorPosY(112f, 1f).SetEase(Ease.OutBounce);
        QuizFlag = new UniTaskCompletionSource<bool>();
        await QuizFlag.Task;
        QuizFlag = null;
        panel.SetActive(false);

        //パネルをしまう
        Destroy(panel);
    }


    /// <summary>
    /// 解答と解説が書かれたパネルを表示する
    /// Enterキーが押されるまで
    /// </summary>
    /// <param name="quiz">表示する問題インスタンス</param>
    public async UniTask DisplayAnswerPanel(IQuiz quiz)
    {
        //対象のパネルを生成（初期状態で非Active）
        GameObject panel = Instantiate(_answerPanel, _canvasObject.transform);

        //パネルの設定
        panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = quiz.CorrectNumber.ToString();
        panel.transform.GetChild(2).GetComponent<Text>().text = quiz.AnswerSentence;

        //Enterキーが押されるまでパネルを表示する
        panel.SetActive(true);
        await DOTween.Sequence()
            .AppendInterval(3f)
            .AppendCallback(() =>
            {
                //正解の選択肢番号を表示する
                _seManager.PlaySE(SEManager.SE.Answer);
                panel.transform.GetChild(1).gameObject.SetActive(true);
                panel.transform.GetChild(1).DOPunchScale(new Vector3(1.1f, 1.1f, 1f), 1f);
            })
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                //解説文を表示する
                panel.transform.GetChild(2).gameObject.SetActive(true);
                panel.transform.GetChild(2).GetComponent<Text>().DOFade(1f,1f);
            })
            .AppendInterval(2f)
            .AppendCallback(() =>
            {
                //「Enterで次へ」を表示する
                panel.transform.GetChild(3).gameObject.SetActive(true);
            });

        //進行役がEnterキーを押すまで待機する
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        //パネルをしまう
        _=panel.GetComponent<RectTransform>().DOAnchorPosY(350f, 1f).SetEase(Ease.OutExpo)
            .OnComplete(() => 
            {
                panel.SetActive(false);
                Destroy(panel);
            });
    }


    /// <summary>
    /// タイムアップのパネルを表示する
    /// 一定時間経過まで
    /// </summary>
    public async UniTask DisplayTimeUpPanel()
    {
        //対象のパネルを生成（初期状態で非Active）
        GameObject panel = Instantiate(_timeUpPanel, _canvasObject.transform);

        //パネルを一定時間表示させてから非表示に戻す
        panel.SetActive(true);
        await DOTween.Sequence()
            .Append(panel.GetComponent<RectTransform>().DOShakeAnchorPos(1f, 15f))
            .AppendInterval(1f)
            .Append(panel.GetComponent<Image>().DOFade(0f, 0.5f));
        panel.SetActive(false);

        //パネルをしまう
        Destroy(panel);
    }


    /// <summary>
    /// thanksパネルを表示
    /// ゲーム終了まで
    /// </summary>
    public void DisplayThanksPanel()
    {
        //対象のパネルを生成
        Instantiate(_thanksPanel, _canvasObject.transform);
    }


    /// <summary>
    /// 演出用：10秒毎に鳥を飛ばす
    /// ゲーム終了まで
    /// </summary>
    public void SpawnBird()
    {
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                //x座標のみ一定範囲内からランダムな座標に設定
                float xOffset = UnityEngine.Random.Range(0, 12);
                var bird =Instantiate(_bird);
                bird.transform.position += new Vector3(xOffset, 0,0);
            })
            .AppendInterval(10)
            .SetLoops(-1);
    }


    /// <summary>
    /// 演出用：90秒毎にドラゴンを飛ばす
    /// ゲーム終了まで
    /// </summary>
    public void SpawnDragon()
    {
        DOTween.Sequence()
            .AppendInterval(90)
            .AppendCallback(() =>
            {
                Instantiate(_dragon);
            })
            .SetLoops(-1);
    }
}
