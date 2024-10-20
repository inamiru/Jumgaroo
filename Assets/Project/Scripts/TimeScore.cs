using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScore : MonoBehaviour
{
    public StageData stageData;  // ステージデータの参照
    public float clearTime;      // プレイヤーのクリアタイム

     // クリアタイムを記録する
    public void SetClearTime(float time)
    {
        clearTime = time;
    }

    // スコアを計算するためにクリアタイムをScoreManagerに渡す
    public void CalculateFinalScore()
    {
        if (TotalScoreCalculator.Instance != null)
        {
            if (stageData != null)
            {
                float baseTime = stageData.GetBaseTime(); // ステージの基準タイムを取得
                TotalScoreCalculator.Instance.CalculateTotalScore(clearTime);
            }
            else
            {
                Debug.LogError("Stage data is not assigned!");
            }
        }
        else
        {
            Debug.LogError("TotalScoreCalculator instance is missing!");
        }
    }
}
