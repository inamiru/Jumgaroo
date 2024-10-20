using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalScoreCalculator : MonoBehaviour
{
    public static TotalScoreCalculator Instance { get; private set; }

    private int maxScore = 1000;    // 最大スコア（基準タイムをピッタリでクリアした時のスコア）
    private int minScore = 100;     // 最低スコア（基準タイムを超えた時のスコア）
    private int itemScore = 0;      // アイテム取得によるスコア
    private int finalScore = 0;     // 最終スコア

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

    // クリアタイムに基づいてスコアを計算するメソッド
    public int CalculateClearTimeScore(float clearTime)
    {
        if (StageData.Instance != null)
        {
            float baseTime = StageData.Instance.GetBaseTime();  // StageDataから基準タイムを取得

            if (clearTime <= baseTime)
            {
                // 基準タイムを下回った場合、クリアタイムに応じてスコアを計算
                return Mathf.FloorToInt(Mathf.Lerp(maxScore, minScore, clearTime / baseTime));
            }
            else
            {
                return minScore;
            }
        }
        else
        {
            Debug.LogError("StageData instance is missing!");
            return minScore;  // 基準タイムが取得できない場合、最低スコア
        }
    }

    // 最終スコアを計算するメソッド
    public void CalculateTotalScore(float clearTime)
    {
        int clearTimeScore = CalculateClearTimeScore(clearTime);
        finalScore = clearTimeScore + itemScore;
    }

    // スコアに応じて星の数を評価
    public int GetStarRating()
    {
        int starCount = 1;  // 最低でも1つ星

        if (finalScore >= 4000)
        {
            starCount = 3;
        }
        else if (finalScore >= 3000)
        {
            starCount = 2;
        }

        return starCount;
    }

    // アイテムスコアを追加するメソッド
    public void AddItemScore(int score)
    {
        if (score > 0)
        {
            itemScore += score;
        }
    }

    // スコアをリセットするメソッド
    public void ResetScores()
    {
        itemScore = 0;
        finalScore = 0;
    }
}
