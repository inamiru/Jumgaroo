using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    public Image[] stars; // 星のImage配列（3つ）

    public float delayBeforeFirstStar = 2f;  // 最初の星を表示する前の遅延時間
    public float starDisplayInterval = 0.5f;   // 星を表示する間隔

    // Start is called before the first frame update
    private void Start()
    {
        // 最初は星を非表示にしておく
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }
    }

    // スコアに応じた星を表示する
    public void ShowStars()
    {
        int starCount = TotalScoreCalculator.Instance.GetStarRating();
        StartCoroutine(DisplayStars(starCount));
    }

    // 指定された数の星を順番に表示する
    private IEnumerator DisplayStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].gameObject.SetActive(true);  // 星を表示
            yield return new WaitForSeconds(starDisplayInterval);  // 次の星の表示を遅延させる
        }
    }
}
