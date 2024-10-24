using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplayManager : MonoBehaviour
{
    public Image[] heartImages; // ハートのUI（Imageコンポーネント）を格納する配列
    public float scaleAmount = 1.2f; // 拡大するスケール
    public float scaleDuration = 0.5f; // 拡大・縮小にかかる時間
    public float scaleInterval = 1.0f; // 拡縮を繰り返す間隔

    private PlayerHealthManager playerHealthManager;

    private void Start()
    {
        playerHealthManager = FindObjectOfType<PlayerHealthManager>(); // PlayerHealthManagerを取得

        // 各ハートを初期化
        UpdateHeartUI(0);
        StartCoroutine(ScaleHearts());
    }

    // ハートUIを更新するメソッド
    public void UpdateHeartUI(int currentHitCount)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHitCount)
            {
                heartImages[i].enabled = true; // ライフが残っている場合、ハートを表示
            }
            else
            {
                heartImages[i].enabled = false; // ライフが減った場合、ハートを非表示
            }
        }
    }

    // ハートのスケールを拡縮するコルーチン
    private IEnumerator ScaleHearts()
    {
        while (true)
        {
            // 現在のスケールを取得
            Vector3 originalScale = heartImages[0].transform.localScale;
            Vector3 targetScale = originalScale * scaleAmount;

            // 拡大アニメーション
            yield return ScaleAllHearts(targetScale, scaleDuration);
            // 縮小アニメーション
            yield return ScaleAllHearts(originalScale, scaleDuration);

            yield return new WaitForSeconds(scaleInterval); // 次の拡縮まで待つ
        }
    }

    // 全てのハートを指定した時間でスケールを変更するメソッド
    private IEnumerator ScaleAllHearts(Vector3 targetScale, float duration)
    {
        float elapsed = 0f;

        // 各ハートのスケールを変更
        while (elapsed < duration)
        {
            for (int i = 0; i < heartImages.Length; i++)
            {
                if (heartImages[i].enabled) // ハートが表示されている場合
                {
                    heartImages[i].transform.localScale = Vector3.Lerp(heartImages[i].transform.localScale, targetScale, elapsed / duration);
                }
            }
            elapsed += Time.deltaTime;
            yield return null; // フレームごとに待機
        }

        // 最終的なスケールを設定
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].enabled)
            {
                heartImages[i].transform.localScale = targetScale; // 最終的なスケールを設定
            }
        }
    }
}
