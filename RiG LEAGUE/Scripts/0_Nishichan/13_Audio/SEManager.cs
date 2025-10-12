using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// SEを管理するクラス
/// </summary>
/// <author>西田琉</author>
public class SEManager : MonoBehaviour
{
    //SEのクリップ一覧
    [SerializeField] private AudioClip _quizStart; //クイズの出題音
    [SerializeField] private AudioClip _selectNumber; //選択音
    [SerializeField] private AudioClip _aori; //煽り音
    [SerializeField] private AudioClip _damage; //ダメージ音
    [SerializeField] private AudioClip _coin; //コイン音
    [SerializeField] private AudioClip _jump; //ジャンプ音
    [SerializeField] private AudioClip _timeUp; //タイムアップ音
    [SerializeField] private AudioClip _answer; //正解発表音
    [SerializeField] private AudioClip _rank3; //3位発表音
    [SerializeField] private AudioClip _rank2; //2位発表音
    [SerializeField] private AudioClip _rank1; //1位発表音


    //SE選択用
    public enum SE
    {
        QuizStart, //クイズの出題音
        SelectNumber, //選択音
        Aori, //煽り音
        Damage, //ダメージ音
        Coin, //コイン音
        Jump, //ジャンプ音
        TimeUp, //タイムアップ音
        Answer, //正解発表音
        Rank3, //3位発表音
        Rank2, //2位発表音
        Rank1, //1位発表音
    }


    /// <summary>
    /// SEを再生する
    /// </summary>
    /// <param name="se">再生するSE</param>
    public async void PlaySE(SE se)
    {
        //SE再生用のAudioSourceを動的に作成
        var source = gameObject.AddComponent<AudioSource>();

        //再生するSEを引数から決定
        switch (se)
        {
            //クイズの出題音
            case SE.QuizStart:
                source.clip = _quizStart;
                break;
            //選択音
            case SE.SelectNumber:
                source.clip = _selectNumber;
                break;
            //煽り音
            case SE.Aori:
                source.clip = _aori;
                break;
            //ダメージ音
            case SE.Damage:
                source.clip = _damage;
                break;
            //コイン音
            case SE.Coin:
                source.clip = _coin;
                break;
            //ジャンプ音
            case SE.Jump:
                source.clip = _jump;
                break;
            //タイムアップ音
            case SE.TimeUp:
                source.clip = _timeUp;
                break;
            //正解発表音
            case SE.Answer:
                source.clip = _answer;
                break;
            //3位発表音
            case SE.Rank3:
                source.clip = _rank3;
                break;
            //2位発表音
            case SE.Rank2:
                source.clip = _rank2;
                break;
            //1位発表音
            case SE.Rank1:
                source.clip = _rank1;
                break;
        }

        //SEを再生し、終わったらAudioSourceを削除
        source.Play();
        await UniTask.WaitUntil( () => !source.isPlaying );
        Destroy(source);
    }
}