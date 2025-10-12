using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SEManager;

/// <summary>
/// BGMを管理するクラス
/// </summary>
/// <author>西田琉</author>
public class BGMManager : MonoBehaviour
{
    //キャッシュ
    private AudioSource[] _audioSource;
    private AudioSource _intro; //イントロ用
    private AudioSource _loop1; //ループ用1
    //private AudioSource _loop2; //ループ用2

    //BGMのクリップ一覧
    [SerializeField] private AudioClip _waitLoop; //待機Loop
    [SerializeField] private AudioClip _gameIntro; //ゲームIntro
    [SerializeField] private AudioClip _gameLoop; //ゲームLoop


    //BGM選択用
    public enum BGM
    {
        WaitLoop, //待機Loop
        Game, //ゲームIntro => ゲームLoop
    }


    private void Awake()
    {
        _audioSource = GetComponents<AudioSource>();
        _intro = _audioSource[0];
        _loop1 = _audioSource[1];
        //_loop2 = _audioSource[2];
    }


    /// <summary>
    /// BGMを再生する
    /// 音が途切れないようにきれいにループする方法確立できたら変更する
    /// </summary>
    /// <param name="se">再生するBGM</param>
    public void PlayBGM(BGM bgm)
    {
        //再生するBGMを引数から決定
        switch (bgm)
        {
            //待機Loop
            case BGM.WaitLoop:
                _loop1.clip = _waitLoop;
                _loop1.Play();
                break;
            //ゲーム
            case BGM.Game:
                //ゲームIntroを一度だけ再生
                float startTime = (float)AudioSettings.dspTime;
                _intro.clip = _gameIntro;
                _intro.PlayScheduled(startTime);
                //ゲームLoopをループ再生
                //bool useLoop1 = true;
                _loop1.clip = _gameLoop;
                //_loop2.clip = _gameLoop;
                _loop1.PlayScheduled(startTime + _gameIntro.length - 0.05f);

                /*
                //イントロが終わるまで次回ループのセットを待機
                await UniTask.Delay(TimeSpan.FromSeconds(_gameIntro.length - 0.05f));

                //ゲームループの長さ毎に次回のBGMスケジュールをセットしておく
                _=DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        float currentTime = (float)AudioSettings.dspTime;
                        if (useLoop1) _loop2.PlayScheduled(currentTime + _gameLoop.length - 0.05f);
                        else _loop1.PlayScheduled(currentTime + _gameLoop.length - 0.05f);
                        useLoop1 = !useLoop1;
                    })
                    .AppendInterval(_gameLoop.length - 0.05f)
                    .SetLoops(-1);*/



                /*
                bool isFirst = true;
                DOTween.Sequence()
                    .AppendCallback(() => 
                    {
                        _loop.clip = _gameLoop;
                        if (isFirst)
                        {
                            loopTime = startTime + _gameIntro.length - 0.05f;
                        }
                        else
                        {
                            float start = (float)AudioSettings.dspTime;

                        }
                        _loop.PlayScheduled(loopTime);
                        isFirst = false;
                    })
                    .SetLoops(-1);*/
                break;
        }
    }
}
