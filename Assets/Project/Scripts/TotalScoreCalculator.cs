using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoreCalculator : MonoBehaviour
{
    public static TotalScoreCalculator Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーンをまたいでオブジェクトを保持
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 合計スコアを計算
    public int CalculateTotalScore()
    {
        int timeScore = (int)TimeScore.Instance.GetFinishTimeScore();
        int itemScore = ItemScore.Instance.GetFinalScore();

        // 合計スコアはタイムスコアとアイテムスコアの合計
        return itemScore + timeScore;
    }
}
