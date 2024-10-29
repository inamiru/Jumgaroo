using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;  // 結果表示用のTextMeshPro UI
    [SerializeField] private TextMeshProUGUI scoreText;  // スコアを表示するTextMeshProUGUI

    [SerializeField] private float timeDisplayDelay = 1.0f;   // クリアタイム表示までの遅延時間（秒）
    [SerializeField] private float scoreDisplayDelay = 2.0f;  // スコア表示までの遅延時間（秒）
    [SerializeField] private float starDisplayDelay = 2.5f;   // 星表示までの遅延時間（秒）

    [SerializeField] private Image[] stars;           // 星のImage配列（3つ）
    [SerializeField] private Transform[] starPositions; // 各星の表示位置
    [SerializeField] private float starDisplayInterval = 0.5f; // 星を表示する間隔

    private bool allResultsDisplayed = false;         // すべての結果が表示されたかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        HideAllStars();  // 星をすべて非表示にする
        StartCoroutine(DisplayResults());  // コルーチンで一連の流れを管理
    }

    // 一連の流れを1つのコルーチンで管理
    private IEnumerator DisplayResults()
    {
        // クリアタイムの表示
        yield return new WaitForSeconds(timeDisplayDelay);
        DisplayClearTime();

        // スコアの表示
        yield return new WaitForSeconds(scoreDisplayDelay);
        DisplayScore();

        // 星の表示をコルーチンとして呼び出す
        yield return StartCoroutine(DisplayStars());

        allResultsDisplayed = true;  // すべての結果が表示されたことを記録
    }

    // クリアタイムを表示するメソッド
    private void DisplayClearTime()
    {
        if (GameTimeDisplay.Instance == null) return;

        float finishTime = GameTimeDisplay.Instance.GetFinishTime();
        int minutes = Mathf.FloorToInt(finishTime / 60);
        int seconds = Mathf.FloorToInt(finishTime % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // スコアを表示するメソッド
    private void DisplayScore()
    {
        if (ItemScore.Instance == null || TotalScoreCalculator.Instance == null || StarCalculator.Instance == null) return;

        int finalScore = ItemScore.Instance.GetFinalScore();
        scoreText.text = finalScore.ToString();

        int totalScore = TotalScoreCalculator.Instance.CalculateTotalScore();
        StarCalculator.Instance.CalculateStarRating(totalScore);  // 星の数を計算
    }

    // 星を表示するメソッド
    private IEnumerator DisplayStars()
    {
        if (StarCalculator.Instance == null || EffectManager.Instance == null) yield break;

        int starCount = StarCalculator.Instance.GetStarCount();
        starCount = Mathf.Min(starCount, stars.Length, starPositions.Length);

        // 最初の星を表示する前に遅延を追加
        yield return new WaitForSeconds(starDisplayDelay);

        // 星を表示する処理
        for (int i = 0; i < starCount; i++)
        {
            if (stars[i] != null)
            {
                stars[i].gameObject.SetActive(true);  // 星を表示
                EffectManager.Instance.PlayStarEffect(starPositions[i].position);  // エフェクトを再生
            }
            yield return new WaitForSeconds(starDisplayInterval);  // インターバルをおいて表示
        }
    }

    // すべての結果が表示されているか確認するメソッド
    public bool AreAllResultsDisplayed() => allResultsDisplayed;

    // 星をすべて非表示にするメソッド
    private void HideAllStars()
    {
        foreach (var star in stars)
        {
            if (star != null)
            {
                star.gameObject.SetActive(false);  // 星を非表示にする
            }
        }
    }
}
