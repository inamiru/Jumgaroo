using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScore : MonoBehaviour
{
    [SerializeField]
    private int maxScore = 1000;    // 最大スコア
    [SerializeField]
    private int minScore = 100;     // 最低スコア
    private float finishTimeScore;

    public static TimeScore Instance { get; private set; }

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

    public void SetFinishTime(float time)
    {
        finishTimeScore = time;
    }

    public float GetFinishTime()
    {
        return finishTimeScore;
    }
    // クリアタイムに基づいてスコアを計算するメソッド
    public int CalculateScore()
    {
        // 依存関係の確認
        if (GameTimeDisplay.Instance == null || StageData.Instance == null)
        {
            Debug.LogError("GameTimeDisplay or StageData instance is missing!");
            return minScore;  // 最低スコアを返す
        }

        float finishTime = GameTimeDisplay.Instance.GetFinishTime();
        float baseTime = StageData.Instance.GetBaseTime();  // ベースタイムを取得

        // クリアタイムがベースタイム以下の場合のスコア計算
        float timeDifference = baseTime - finishTime;
        int score = Mathf.FloorToInt(timeDifference * (maxScore / baseTime));

        // スコアをminScore以上、maxScore以下に制限
        score = Mathf.Clamp(score, minScore, maxScore);

        return score;
    }
}
