using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvincibilityManager : MonoBehaviour
{
    [SerializeField] private float invincibilityDuration = 2f; // 無敵時間（秒）
    private bool isInvincible = false;

    private Renderer playerRenderer;
    private Coroutine invincibilityCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponent<Renderer>();
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    public void StartInvincibility()
    {
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }

        invincibilityCoroutine = StartCoroutine(InvincibilityCoroutine());
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        float elapsedTime = 0f;

        // 点滅エフェクト
        Color originalColor = playerRenderer.material.color;
        while (elapsedTime < invincibilityDuration)
        {
            playerRenderer.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f); // 半透明
            yield return new WaitForSeconds(0.2f);

            playerRenderer.material.color = originalColor; // 元の色に戻す
            yield return new WaitForSeconds(0.2f);

            elapsedTime += 0.4f;
        }

        isInvincible = false;
    }
}
