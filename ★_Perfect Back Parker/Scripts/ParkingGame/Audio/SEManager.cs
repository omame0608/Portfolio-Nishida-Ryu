using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParkingGame.Audio
{
    /// <summary>
    /// SE管理クラス
    /// </summary>
    public class SEManager : MonoBehaviour
    {
        //SEのクリップ一覧
        [SerializeField] private AudioClip _klaxon; //クラクション
        [SerializeField] private AudioClip _timer1; //カチカチ1
        [SerializeField] private AudioClip _timer2; //カチカチ2
        [SerializeField] private AudioClip _alert; //アラート
        [SerializeField] private AudioClip _crash; //クラッシュ
        [SerializeField] private AudioClip _circle1; //サークル1周目
        [SerializeField] private AudioClip _circle2; //サークル2周目
        [SerializeField] private AudioClip _circle3; //サークル3周目
        [SerializeField] private AudioClip _clear; //ステージクリア
        [SerializeField] private AudioClip _ending; //エンディング


        /// <summary>
        /// 指定したSEを再生する
        /// </summary>
        /// <param name="se">再生するSE</param>
        public async void PlaySE(SE se)
        {
            //SE再生用のAudioSourceを動的に作成
            var source = gameObject.AddComponent<AudioSource>();

            //指定されたSEを設定
            switch (se)
            {
                //クラクション
                case SE.Klaxon:
                    source.clip = _klaxon;
                    source.volume = 0.5f;
                    break;
                //カチカチ1
                case SE.Timer1:
                    source.clip = _timer1;
                    source.volume = 0.15f;
                    break;
                //カチカチ2
                case SE.Timer2:
                    source.clip = _timer2;
                    source.volume = 0.15f;
                    break;
                //アラート
                case SE.Alert:
                    source.clip = _alert;
                    break;
                //クラッシュ
                case SE.Crash:
                    source.clip = _crash;
                    source.volume = 0.4f;
                    break;
                //サークル1周目
                case SE.Circle1:
                    source.clip = _circle1;
                    source.volume = 0.5f;
                    break;
                //サークル2周目
                case SE.Circle2:
                    source.clip = _circle2;
                    source.volume = 0.5f;
                    break;
                //サークル3周目
                case SE.Circle3:
                    source.clip = _circle3;
                    source.volume = 0.5f;
                    break;
                //ステージクリア
                case SE.Clear:
                    source.clip = _clear;
                    source.volume = 0.5f;
                    break;
                //エンディング
                case SE.Ending:
                    source.clip = _ending;
                    source.volume = 1f;
                    break;
            }

            //SEを再生し、終わったらAudioSourceを削除
            source.Play();
            await UniTask.WaitUntil(() => !source.isPlaying);
            Destroy(source);
        }
    }


    /// <summary>
    /// SE選択用
    /// </summary>
    public enum SE
    {
        Klaxon, //クラクション
        Timer1, //カチカチ1
        Timer2, //カチカチ2
        Alert, //アラート
        Crash, //クラッシュ
        Circle1, //サークル1周目
        Circle2, //サークル2周目
        Circle3, //サークル3周目
        Clear, //ステージクリア
        Ending, //エンディング
    }
}