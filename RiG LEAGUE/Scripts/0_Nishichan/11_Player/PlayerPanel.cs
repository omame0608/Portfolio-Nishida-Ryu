using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 各プレイヤー用のUIを制御するクラス
/// </summary>
/// <author>西田琉</author>
public class PlayerPanel : MonoBehaviour
{
    //UI下段のプレイヤー状態表示
    public enum State
    {
        Preparing, //準備中(初期状態)
        None,      //非表示状態
        NoAnswer,  //未回答
        Answered,  //回答済
        Correct,   //正解！
        Incorrect, //不正解
        Jump,      //飛べ！
        Get        //取れ！
    }

    //参照一覧
    [SerializeField] private Text _scoreText; //スコアテキスト
    [SerializeField] private Text _infoText; //状態表示テキスト

    //キャッシュ
    private RectTransform _infoRectTransform; //状態表示テキストのキャッシュ
    private Vector3 _defaultScale; //初期状態でのサイズ

    //プレイヤー状態
    public State Status {  get; private set; }


    private void Start()
    {
        //プレイヤーの状態に初期状態をセットする
        TransitionState(State.Preparing);

        //キャッシュを取得
        _infoRectTransform = transform.GetChild(1).GetComponent<RectTransform>();
        _defaultScale = _infoRectTransform.localScale;
    }


    /// <summary>
    /// プレイヤーの状態を更新
    /// </summary>
    /// <param name="state">変更後の状態</param>
    public void TransitionState(State state)
    {
        //自身の状態を変更
        Status = state;

        //UI表示を変更
        switch (state)
        {
            case State.Preparing:
                _infoText.text = "準備中";
                _infoText.color = Color.white; //白
                break;
            case State.None:
                _infoText.text = "";
                _infoText.color = Color.white; //白
                break;
            case State.NoAnswer:
                _infoText.text = "未回答";
                _infoText.color = Color.gray; //グレー
                break;
            case State.Answered:
                _infoText.text = "回答済";
                _infoText.color = Color.black; //黒
                _infoRectTransform.DOKill();
                _infoRectTransform.localScale = _defaultScale;
                _infoRectTransform.DOShakeScale(1f, 1.1f);
                break;
            case State.Correct:
                _infoText.text = "正解!!";
                _infoText.color = Color.red; //赤
                _infoRectTransform.DOPunchScale(new Vector2(1.2f, 1.2f), 1f);
                break;
            case State.Incorrect:
                _infoText.text = "不正解";
                _infoText.color = Color.blue; //青
                _infoRectTransform.DOShakePosition(1f, 10f);
                break;
            case State.Jump:
                _infoText.text = "飛べ!!";
                _infoText.color = Color.green; //緑
                _infoRectTransform.DOScale(new Vector2(1.4f, 1.4f), 1f).SetEase(Ease.OutElastic)
                    .OnComplete(() => _infoRectTransform.DOScale(new Vector2(1f, 1f), 0.5f));
                break;
            case State.Get:
                _infoText.text = "取れ!!";
                _infoText.color = Color.yellow; //黄
                _infoRectTransform.DOScale(new Vector2(1.4f, 1.4f), 1f).SetEase(Ease.OutElastic)
                    .OnComplete(() => _infoRectTransform.DOScale(new Vector2(1f, 1f), 0.5f));
                break;
        }
    }


    /// <summary>
    /// スコアテキストを更新
    /// </summary>
    /// <param name="score">更新後の値</param>
    public void UpdateScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
}
