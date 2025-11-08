using Alchemy.Inspector;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PerfectBackParkerRev.Audios
{
    /// <summary>
    /// SEマネージャー
    /// </summary>
    public class SEManager : MonoBehaviour
    {
        //SEのクリップ一覧
        [SerializeField] private AudioClip _shortKlaxon; //短いクラクション
        [SerializeField] private AudioClip _engine; //エンジン音
        [SerializeField] private AudioClip _brake; //ブレーキ
        [SerializeField] private AudioClip _stageSelectArrow; //ステージセレクト矢印
        [SerializeField] private AudioClip _stageStopArrow; //ステージセレクト矢印停止
        [SerializeField] private AudioClip _stageEnter; //ステージ決定
        [SerializeField] private AudioClip _stageStart; //ステージ開始
        [SerializeField] private AudioClip _TransitionIn; //トランジションイン
        [SerializeField] private AudioClip _TransitionOut; //トランジションアウト
        [SerializeField] private AudioClip _playerIn; //プレイヤーイン
        [SerializeField] private AudioClip _playerLand; //プレイヤー着地
        [SerializeField] private AudioClip _klaxon; //クラクション
        [SerializeField] private AudioClip _timer1; //カチカチ1
        [SerializeField] private AudioClip _timer2; //カチカチ2
        [SerializeField] private AudioClip _alert; //アラート
        [SerializeField] private AudioClip _screwPickup; //ネジ取得
        [SerializeField] private AudioClip _crash; //クラッシュ
        [SerializeField] private AudioClip _circle1; //サークル1周目
        [SerializeField] private AudioClip _circle2; //サークル2周目
        [SerializeField] private AudioClip _circle3; //サークル3周目
        [SerializeField] private AudioClip _waveClear; //ウェーブクリア
        [SerializeField] private AudioClip _stageClear; //ステージクリア
        [SerializeField] private AudioClip _waveScoreResult; //ウェーブスコア結果音
        [SerializeField] private AudioClip _stageScoreResult; //ステージスコア結果音
        [SerializeField] private AudioClip _gameClear; //ゲームクリア


        private void Awake()
        {
            //SEマネージャーはシーン遷移で破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }


        /// <summary>
        /// 指定したSEを再生する
        /// </summary>
        /// <param name="se">再生するSE</param>
        [Button]
        public async void PlaySE(SE se)
        {
            //SE再生用のAudioSourceを動的に作成
            var source = gameObject.AddComponent<AudioSource>();

            //指定されたSEを設定
            switch (se)
            {
                //短いクラクション
                case SE.ShortKlaxon:
                    source.clip = _shortKlaxon;
                    source.volume = 0.7f;
                    break;
                //エンジン音
                case SE.Engine:
                    source.clip = _engine;
                    source.volume = 1f;
                    break;
                //ブレーキ
                case SE.Brake:
                    source.clip = _brake;
                    source.volume = 0.5f;
                    break;
                //ステージセレクト矢印
                case SE.StageSelectArrow:
                    source.clip = _stageSelectArrow;
                    source.volume = 0.7f;
                    break;
                //ステージセレクト矢印停止
                case SE.StageStopArrow:
                    source.clip = _stageStopArrow;
                    source.volume = 0.7f;
                    break;
                //ステージ決定
                case SE.StageEnter:
                    source.clip = _stageEnter;
                    source.volume = 0.5f;
                    break;
                //ステージ開始
                case SE.StageStart:
                    source.clip = _stageStart;
                    source.volume = 1f;
                    break;
                //プレイヤーイン
                case SE.PlayerIn:
                    source.clip = _playerIn;
                    source.volume = 1f;
                    break;
                //プレイヤー着地
                case SE.PlayerLand:
                    source.clip = _playerLand;
                    source.volume = 1f;
                    break;
                //トランジションイン
                case SE.TransitionIn:
                    source.clip = _TransitionIn;
                    source.volume = 0.7f;
                    break;
                //トランジションアウト
                case SE.TransitionOut:
                    source.clip = _TransitionOut;
                    source.volume = 0.7f;
                    break;
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
                    source.volume = 1f;
                    break;
                //ネジ取得
                case SE.ScrewPickup:
                    source.clip = _screwPickup;
                    source.volume = 1f;
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
                //ウェーブクリア
                case SE.WaveClear:
                    source.clip = _waveClear;
                    source.volume = 0.5f;
                    break;
                //ステージクリア
                case SE.StageClear:
                    source.clip = _stageClear;
                    source.volume = 1f;
                    break;
                //ウェーブスコア結果音
                case SE.WaveScoreResult:
                    source.clip = _waveScoreResult;
                    source.volume = 0.4f;
                    break;
                //ステージスコア結果音
                case SE.StageScoreResult:
                    source.clip = _stageScoreResult;
                    source.volume = 1f;
                    break;
                //ゲームクリア
                case SE.GameClear:
                    source.clip = _gameClear;
                    source.volume = 0.3f;
                    break;
            }

            //SEを再生し、終わったらAudioSourceを削除
            source.Play();
            await UniTask.WaitUntil(() => !source.isPlaying);
#if UNITY_EDITOR
            DestroyImmediate(source);
#else
            Destroy(source);
#endif
        }


        public void StopAllSE()
        {
            //全てのSEを停止する
            var sources = GetComponents<AudioSource>();
            foreach (var source in sources)
            {
                //消音で流して消す
                source.volume = 0f;
            }
        }
    }


    /// <summary>
    /// SE選択用
    /// </summary>
    public enum SE
    {
        ShortKlaxon, //短いクラクション
        Engine, //エンジン音

        Brake, //ブレーキ
        StageSelectArrow, //ステージセレクト矢印
        StageStopArrow, //ステージセレクト矢印停止
        StageEnter, //ステージ決定
        TransitionIn, //トランジションイン
        TransitionOut, //トランジションアウト

        StageStart, //ステージ開始
        PlayerIn, //プレイヤーイン
        PlayerLand, //プレイヤー着地
        Klaxon, //クラクション
        Timer1, //カチカチ1
        Timer2, //カチカチ2
        Alert, //アラート
        ScrewPickup, //ネジ取得
        Crash, //クラッシュ
        Circle1, //サークル1周目
        Circle2, //サークル2周目
        Circle3, //サークル3周目
        WaveClear, //ウェーブクリア
        StageClear, //ステージクリア
        WaveScoreResult, //ウェーブスコア結果音
        StageScoreResult, //ステージスコア結果音

        GameClear, //ゲームクリア
    }
}