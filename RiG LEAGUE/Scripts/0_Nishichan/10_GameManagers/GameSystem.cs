using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.InputSystem;
using VContainer;
using DG.Tweening;
using Unity.VisualScripting;
using System.Linq;
using Cysharp.Threading.Tasks.Triggers;

/// <summary>
/// ゲームの進行を担うクラス
/// </summary>
/// <author>西田琉</author>
public class GameSystem : VContainer.Unity.IInitializable
{
    //ファサード一覧
    [Inject] private IFieldScrollSystemFacade _fieldScrollSystemFacade;
    [Inject] private ITimerSystemFacade _timerSystemFacade;
    [Inject] private IQuizSystemFacade _quizSystemFacade;
    [Inject] private GameViewController _gameViewController;

    //ファクトリ一覧
    [Inject] private LogFactory _logFactory;
    [Inject] private CoinFactory _coinFactory;

    //Audio
    [Inject] private SEManager _seManager;
    [Inject] private BGMManager _bgmManager;

    //総問題数　テスト時は0か1に
    private const int _QUIZ_All = 10;

    //解答解説が表示されてから各プレイヤーの正解・不正解が表示されるまでの時間
    private const float _WAIT_JUDGE = 3f;

    //プレイヤー一覧
    public List<TrolleyController> TrolleyList { get; set; } = new List<TrolleyController>();


    public async void Initialize()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));

        //待機BGMの再生を開始する
        _bgmManager.PlayBGM(BGMManager.BGM.WaitLoop);

        //演出を開始する
        _gameViewController.SpawnBird();
        _gameViewController.SpawnDragon();

        //進行役がEnterキーを押すまで待機する
        Debug.Log($"<color=blue>リモコンを繋げてEnterキーを押して進行開始</color>");
        await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        
        //ゲームBGMを再生し、タイミングを合わせてゲーム進行を開始する
        Debug.Log($"<color=green>ゲームの進行を開始します</color>");
        _bgmManager.PlayBGM(BGMManager.BGM.Game);
        await UniTask.Delay(TimeSpan.FromSeconds(5.8f));
        _fieldScrollSystemFacade.StartFieldScroll(false); //フィールドスクロール開始

        //プレイヤーを動かす
        foreach (var trolley in TrolleyList)
        {
            trolley.SwitchTrolleyMove(true);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        //タイマーを表示する
        GameObject timer = GameObject.FindGameObjectWithTag("Timer");
        _=timer.GetComponent<RectTransform>().DOAnchorPosX(-3.5f, 0.5f);

        //ターン制御
        for (int i = 1; i <= _QUIZ_All; i++)
        {
            await StartTurn(i);
            Debug.Log($"<color=yellow>ターン{i}が終了しました</color>");
        }

        //タイマーを非表示にする
        _=timer.GetComponent<RectTransform>().DOAnchorPosX(-250f, 0.5f);

        //結果発表
        await Result();

        //ゲーム終了パネルを表示する
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        _gameViewController.DisplayThanksPanel();
        Debug.Log($"<color=green>ゲームが終了しました</color>");
    }


    /// <summary>
    /// 出題からミニゲーム終了までを1ターン分進行
    /// </summary>
    /// <param name="turn">進行するターン番号</param>
    private async UniTask StartTurn(int turn)
    {
        //このターンの問題を取得する
        IQuiz quiz = _quizSystemFacade.GetQuizWithID(turn);

        //問題No表示
        _seManager.PlaySE(SEManager.SE.QuizStart);
        await _gameViewController.DisplayQuizNumberPanel(turn);

        //問題表示
        _gameViewController.DisplayQuizPanel(quiz);

        //ユーザからの回答受付を開始する
        foreach (var trolley in TrolleyList)
        {
            trolley.CanAnswer = true;
            trolley.TransitionState(PlayerPanel.State.NoAnswer);
        }

        //カウントを開始する　書き直したい
        _timerSystemFacade.UseTimer(quiz.TimeLimit);
        await UniTask.Delay(TimeSpan.FromSeconds(quiz.TimeLimit));

        //ユーザからの回答受付を終了する
        foreach (var trolley in TrolleyList)
        {
            trolley.CanAnswer = false;
        }

        //タイムアップをしらせる
        _seManager.PlaySE(SEManager.SE.TimeUp);
        await _gameViewController.DisplayTimeUpPanel();

        //全プレイヤーの回答を表示する
        foreach (var trolley in TrolleyList)
        {
            //回答済みであれば表示する
            if (trolley.PlayerPanel.Status == PlayerPanel.State.Answered) trolley.DisplayOwnAnswer();
        }
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        //問題非表示
        _gameViewController.QuizFlag?.TrySetResult(true);

        //全プレイヤーの正誤判定を行う
        foreach (var trolley in TrolleyList)
        {
            //回答済みであれば正誤判定を行う
            if (trolley.PlayerPanel.Status == PlayerPanel.State.Answered)
            {
                var sequense = DOTween.Sequence();

                //正解の場合
                if (quiz.JudgeAnswer(trolley.AnsweredNumber))
                {
                    _=sequense.AppendInterval(_WAIT_JUDGE)
                            .AppendCallback(() => 
                            {
                                trolley.PlayerPanel.TransitionState(PlayerPanel.State.Correct);

                                //スコア加算
                                trolley.AddScore(5);
                            });
                }
                //不正解の場合
                else
                {
                    _=sequense.AppendInterval(_WAIT_JUDGE)
                            .AppendCallback(() => trolley.PlayerPanel.TransitionState(PlayerPanel.State.Incorrect));
                }
            }
        }

        //解答解説表示
        await _gameViewController.DisplayAnswerPanel(quiz);

        //ミニゲーム
        int id = 0; //オブジェクトを流すレールを決めるID
        foreach (var trolley in TrolleyList)
        {
            //自身の解答を非表示にする
            trolley.AnswerDisplayFlag?.TrySetResult(true);

            //正解の場合
            if (trolley.PlayerPanel.Status == PlayerPanel.State.Correct)
            {
                //コインを流す
                trolley.PlayerPanel.TransitionState(PlayerPanel.State.Get);
                _coinFactory.InstantiateCoins(id);
            }

            //不正解または未回答の場合
            if (trolley.PlayerPanel.Status == PlayerPanel.State.Incorrect
                || trolley.PlayerPanel.Status == PlayerPanel.State.NoAnswer)
            {
                //丸太を流す
                trolley.PlayerPanel.TransitionState(PlayerPanel.State.Jump);
                _logFactory.InstantiateLogs(id);
            }
            
            id++;
        }
        await UniTask.Delay(TimeSpan.FromSeconds(9f));


        //全プレイヤーの状態をデフォルトに戻す
        foreach (var trolley in TrolleyList)
        {
            trolley.TransitionState(PlayerPanel.State.None);
            trolley.ResetAnsweredNumber();
        }
    }


    /// <summary>
    /// 結果発表を行う
    /// </summary>
    private async UniTask Result()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        Debug.Log($"<color=green>結果を発表します</color>");

        //プレイヤーリストをスコア順にソートする
        TrolleyController[] rankedArray = TrolleyList.OrderByDescending(t => t.Score).ToArray();
        
        //順位を保存する配列を用意
        int[] rankings = new int[rankedArray.Length];
        int currentRank = 0; //現在の順位
        int lastPlayerPoint = 9999; //自身の前の人の点数、同点なら順位を進めずタイとする
        
        //順位配列の作成
        for (int i = 0; i < rankedArray.Length; i++)
        {
            //付ける順位を決める
            if (rankedArray[i].Score != lastPlayerPoint) currentRank++;

            //順位を記録
            rankings[i] = currentRank;
            rankedArray[i].Ranking = currentRank;

            //次回のためにスコアを保持
            lastPlayerPoint = rankedArray[i].Score;
        }

        //UIを表示
        for (int i = 0; i < rankedArray.Length; i++)
        {
            Debug.Log($"{rankings[i]}位：{rankedArray[i].Score}点");
            rankedArray[i].DisplayRank(rankings[i]);
        }
    }
}
