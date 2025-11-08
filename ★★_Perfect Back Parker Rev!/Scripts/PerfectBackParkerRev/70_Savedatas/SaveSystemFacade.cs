using PerfectBackParkerRev.GameCores.Repositories;
using UnityEngine;
using VContainer.Unity;

namespace PerfectBackParkerRev.Savedatas
{
    /// <summary>
    /// セーブシステムの窓口
    /// </summary>
    public class SaveSystemFacade : IInitializable, ISaveSystemFacade
    {
        void IInitializable.Initialize()
        {
            //永続化データ宣言
            if (!PlayerPrefs.HasKey(SaveKey.Stage1.ToString()))
            {
                PlayerPrefs.SetInt(SaveKey.Stage1.ToString(), 0);
            }
            if (!PlayerPrefs.HasKey(SaveKey.Stage2.ToString()))
            {
                PlayerPrefs.SetInt(SaveKey.Stage2.ToString(), 0);
            }
            if (!PlayerPrefs.HasKey(SaveKey.Stage3.ToString()))
            {
                PlayerPrefs.SetInt(SaveKey.Stage3.ToString(), 0);
            }
            if (!PlayerPrefs.HasKey(SaveKey.Stage4.ToString()))
            {
                PlayerPrefs.SetInt(SaveKey.Stage4.ToString(), 0);
            }
            if (!PlayerPrefs.HasKey(SaveKey.Stage5.ToString()))
            {
                PlayerPrefs.SetInt(SaveKey.Stage5.ToString(), 0);
            }
        }


        public void SaveStageHighScore(SaveKey stageKey, int newHighScore)
        {
            if (stageKey == SaveKey.None) return;

            //セーブする
            PlayerPrefs.SetInt(stageKey.ToString(), newHighScore);
            PlayerPrefs.Save();
        }


        public int LoadStageHighScore(SaveKey stageKey)
        {
            if (stageKey == SaveKey.None) return 0;

            //ロードして返す
            return PlayerPrefs.GetInt(stageKey.ToString(), 0);
        }


        public int GetTotalGameScore()
        {
            int totalScore = 0;
            totalScore += LoadStageHighScore(SaveKey.Stage1);
            totalScore += LoadStageHighScore(SaveKey.Stage2);
            totalScore += LoadStageHighScore(SaveKey.Stage3);
            totalScore += LoadStageHighScore(SaveKey.Stage4);
            totalScore += LoadStageHighScore(SaveKey.Stage5);
            return totalScore;
        }
    }
}