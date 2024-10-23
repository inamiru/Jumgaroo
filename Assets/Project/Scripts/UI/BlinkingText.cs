using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;  // 点滅させたいTextMeshProの参照
    public float blinkInterval = 1.0f;   // 点滅間隔（秒）
    private bool isBlinking = false;     // 点滅中かどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        if (textToBlink != null)
        {
            StartCoroutine(BlinkText());
        }
    }

    IEnumerator BlinkText()
    {
        isBlinking = true;
        
        while (isBlinking)
        {
            // アルファ値を0（透明）に設定
            textToBlink.alpha = 0f;

            // blinkInterval秒待機
            yield return new WaitForSeconds(blinkInterval);

            // アルファ値を1（完全に表示）に設定
            textToBlink.alpha = 1f;

            // blinkInterval秒待機
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    // 点滅を停止するメソッド
    public void StopBlinking()
    {
        isBlinking = false;
        textToBlink.alpha = 1f;  // 点滅終了後にテキストを表示状態に戻す
    }
}
