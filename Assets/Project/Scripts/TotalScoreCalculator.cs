using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoreCalculator : MonoBehaviour
{
    public static TotalScoreCalculator Instance { get; private set; }

    [SerializeField] private int twoStarThreshold = 2000;
    [SerializeField] private int threeStarThreshold = 4000;

    private void Awake()
    {
        // シングルトンパターンの確認
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // 合計スコアを計算
    public int CalculateTotalScore()
    {
        int timeScore = TimeScore.Instance.CalculateScore();
        int itemScore = ItemScore.Instance.GetFinalScore();

        // 合計スコアはタイムスコアとアイテムスコアの合計
        return itemScore + timeScore;
    }

    // スコアに応じて星の数を評価
    public int GetStarRating()
    {
        int totalScore = CalculateTotalScore();
        Debug.Log("Total Score: " + totalScore);  // デバッグ用ログ

        // スコアに応じた星の数を返す
        if (totalScore >= threeStarThreshold)
            return 3;  // threeStarThreshold以上で3つ星
        else if (totalScore >= twoStarThreshold)
            return 2;  // twoStarThreshold以上で2つ星
        else
            return 1;  // twoStarThreshold未満で1つ星
    }
}
