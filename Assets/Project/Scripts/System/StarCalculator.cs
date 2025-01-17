using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarCalculator : MonoBehaviour
{
    public static StarCalculator Instance { get; private set; }

    [SerializeField] private int twoStarThreshold = 2000;
    [SerializeField] private int threeStarThreshold = 4000;

    private int starCount = 0;

    // シングルトンの初期化
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // スコアに基づいて星の数を計算するメソッド
    public void CalculateStarRating(int totalScore)
    {
        if (GameTimeDisplay.Instance.isDead_Display)
        {
            starCount = 0;
            return;
        }

        // 星の数をスコアに基づいて決定
        if (totalScore >= 4000)
        {
            starCount = 3;  // 4000以上で3つ星
        }
        else if (totalScore >= 2000)
        {
            starCount = 2;  // 2000以上で2つ星
        }
        else
        {
            starCount = 1;  // 2000未満で1つ星
        }
    }

    // 計算された星の数を取得するメソッド
    public int GetStarCount()
    {
        return starCount;
    }
}
