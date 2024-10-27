using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartDisplayManager : MonoBehaviour
{
    public Image[] heartImages; // ハートのUI（Imageコンポーネント）を格納する配列
    public GameObject heartLostEffectPrefab; // ハート喪失エフェクトのプレハブ
    public Camera UICamera; // UIカメラの参照

    public float scaleAmount = 1.2f; // 拡大するスケール
    public float scaleDuration = 0.5f; // 拡大・縮小にかかる時間
    public float scaleInterval = 1.0f; // 拡縮を繰り返す間隔

    public PlayerStates playerStates;  // ScriptableObject の参照
    private int previousHealth; // 前回のハート数を保持

    private void Start()
    {
        previousHealth = playerStates.maxHits; // 初期のハート数を設定
        UpdateHeartUI(playerStates.currentHitCount);
        StartCoroutine(ScaleHearts()); // スケールエフェクトを開始
    }

    private void Update()
    {
        // ハート数が変わった場合にUIを更新
        if (previousHealth != playerStates.currentHitCount)
        {
            PlayHeartLostEffect(previousHealth - playerStates.currentHitCount); // 減少分のエフェクトを再生
            UpdateHeartUI(playerStates.currentHitCount);
            previousHealth = playerStates.currentHitCount; // 現在のハート数を保持
        }
    }

    // ハートUIを更新するメソッド
    public void UpdateHeartUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentHealth; // ライフが残っている場合、ハートを表示
        }
    }

    // ハート消失エフェクトを表示
    public void PlayHeartLostEffect(int lostHeartsCount)
    {
        for (int i = 1; i <= lostHeartsCount; i++)
        {
            int heartIndex = previousHealth - i;
            if (heartIndex >= 0 && heartIndex < heartImages.Length)
            {
                RectTransform heartRectTransform = heartImages[heartIndex].rectTransform;
                Vector3 screenPoint = UICamera.WorldToScreenPoint(heartRectTransform.position);
                Vector3 worldPoint;

                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(heartRectTransform, screenPoint, UICamera, out worldPoint))
                {
                    EffectManager.Instance.PlayHeartLostEffect(worldPoint);
                }
            }
        }
    }

    private IEnumerator ScaleHearts()
    {
        while (true)
        {
            Vector3 originalScale = heartImages[0].transform.localScale;
            Vector3 targetScale = originalScale * scaleAmount;

            yield return ScaleAllHearts(targetScale, scaleDuration);
            yield return ScaleAllHearts(originalScale, scaleDuration);

            yield return new WaitForSeconds(scaleInterval);
        }
    }

    private IEnumerator ScaleAllHearts(Vector3 targetScale, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            for (int i = 0; i < heartImages.Length; i++)
            {
                if (heartImages[i].enabled)
                {
                    heartImages[i].transform.localScale = Vector3.Lerp(heartImages[i].transform.localScale, targetScale, elapsed / duration);
                }
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < heartImages.Length; i++)
        {
            if (heartImages[i].enabled)
            {
                heartImages[i].transform.localScale = targetScale; // 最終的なスケールを設定
            }
        }
    }
}