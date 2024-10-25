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

    public PlayerStates playerStates;  // ScriptableObject の参照
    private int previousHealth; // 前回のハート数を保持
    private bool hasLostHearts = false; // ハートを失ったかどうかのフラグ]

    // UI用カメラの参照を追加
    public Camera uiCamera;

    private void Start()
    {
        previousHealth = playerStates.maxHits; // 初期のハート数を設定

        // 各ハートを初期化
        UpdateHeartUI(0);
        StartCoroutine(ScaleHearts());
    }

    private void Update()
    {
        int currentHealth = playerStates.currentHitCount; // 現在のハート数を取得
        UpdateHeartUI(currentHealth);

        if (currentHealth < previousHealth && !hasLostHearts) // ハートが減った場合にエフェクトを表示
        {
            PlayHeartLostEffect(previousHealth - currentHealth);
            hasLostHearts = true; // フラグを立てる
        }
        else if (currentHealth >= previousHealth)
        {
            hasLostHearts = false; // ハートが復活したらフラグをリセット
        }

        previousHealth = currentHealth; // 現在のハート数を保持
    }

    // ハートUIを更新するメソッド
    public void UpdateHeartUI(int currentHealth)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < playerStates.maxHits) // maxHits以内であれば表示を切り替える
            {
                heartImages[i].enabled = i < currentHealth; // ライフが残っている場合、ハートを表示
            }
        }
    }

    // 減少したハートの位置にエフェクトを表示する
    public void PlayHeartLostEffect(int lostHeartsCount)
    {
        for (int i = previousHealth - 1; i >= previousHealth - lostHeartsCount; i--)
        {
            if (i >= 0 && i < heartImages.Length)
            {
                // UIカメラでのハート位置を取得
                Vector3 screenPosition = heartImages[i].rectTransform.position;

                // スクリーン座標をワールド座標に変換
                Vector3 worldPosition = uiCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, uiCamera.nearClipPlane));

                // Y軸を調整（必要に応じて調整）
                worldPosition.y += 1f; // UIの上にエフェクトを表示するためのオフセット
                worldPosition.x = 0; // X軸を固定

                // エフェクトを生成
                Instantiate(EffectManager.instance.heartLostEffectPrefab, worldPosition, Quaternion.identity);
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