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
        DisplayStars();
    }

    // クリアタイムを表示するメソッド
    private void DisplayClearTime()
    {
            float finishTime = GameTimeDisplay.Instance.GetFinishTime();

            int minutes = Mathf.FloorToInt(finishTime / 60);
            int seconds = Mathf.FloorToInt(finishTime % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // スコアを表示するメソッド
    private void DisplayScore()
    {
            int finalScore = ItemScore.Instance.GetFinalScore();
            scoreText.text = finalScore.ToString();
    }

    // スコアに応じた星を表示
    private IEnumerator DisplayStars()
    {
        int starCount = TotalScoreCalculator.Instance.GetStarRating();

        // stars 配列のサイズを超えないように制限
        for (int i = 0; i < Mathf.Min(starCount, stars.Length); i++)
        {
            if (stars[i] != null)
            {
                stars[i].gameObject.SetActive(true);  // 星を表示
            }
            yield return new WaitForSeconds(starDisplayInterval);
        }
    }
}
