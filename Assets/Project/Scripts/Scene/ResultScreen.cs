using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResultScreen : MonoBehaviour
{
    public TextMeshProUGUI timeText;  // 結果表示用のTextMeshPro UI
    public TextMeshProUGUI scoreText;  // スコアを表示するTextMeshProUGUI

    public float timeDisplayDelay = 1.0f;   // クリアタイム表示までの遅延時間（秒）
    public float scoredisplayDelay = 2.0f;    // スコア表示までの遅延時間（秒）

    public Image[] stars; // 星のImage配列（3つ）

    public float starDisplayInterval = 0.5f;   // 星を表示する間隔

    void Awake()
    {
        // GameTimeDisplay が存在しない場合にインスタンスを作成
        if (GameTimeDisplay.Instance == null)
        {
            GameObject gameTimeDisplayObject = new GameObject("GameTimeDisplay");
            gameTimeDisplayObject.AddComponent<GameTimeDisplay>();  // GameTimeDisplay を追加
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // 最初は星を非表示にしておく
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        // コルーチンで一連の流れを管理
        StartCoroutine(DisplayResults());
    }

    // 一連の流れを1つのコルーチンで管理
    private IEnumerator DisplayResults()
    {
        // クリアタイムの表示
        yield return new WaitForSeconds(timeDisplayDelay);
        DisplayClearTime();

        // スコアの表示
        yield return new WaitForSeconds(scoredisplayDelay);
        DisplayScore();

        // 星の表示
        ShowStars();
    }

    // クリアタイムを表示するメソッド
private void DisplayClearTime()
{
    if (GameTimeDisplay.Instance != null)
    {
        // GameTimeDisplayからゴール時のタイムを取得
        float finishTime = GameTimeDisplay.Instance.GetFinishTime();

        // デバッグログでfinishTimeを表示
        Debug.Log("FinishTime: " + finishTime);

        int minutes = Mathf.FloorToInt(finishTime / 60);
        int seconds = Mathf.FloorToInt(finishTime % 60);
        
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    else
    {
        Debug.LogError("GameTimeDisplay instance is missing!");
    }
}

    // スコアを表示するメソッド
    private void DisplayScore()
    {
        if (ItemScore.Instance != null)
        {
            int finalScore = ItemScore.Instance.GetFinalScore();
            scoreText.text = finalScore.ToString();
        }
        else
        {
            Debug.LogError("ItemScore instance is missing!");
        }
    }

     // スコアに応じた星を表示するメソッド
    public void ShowStars()
    {
        if (TotalScoreCalculator.Instance != null)
        {
            int starCount = TotalScoreCalculator.Instance.GetStarRating();

            // 星の数が星の配列の長さを超えないことを確認
            starCount = Mathf.Clamp(starCount, 1, stars.Length);

            StartCoroutine(DisplayStars(starCount));
        }
    }

    // 指定された数の星を順番に表示するコルーチン
    private IEnumerator DisplayStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            if (stars[i] != null)
            {
                stars[i].gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(starDisplayInterval);
        }
    }
}
