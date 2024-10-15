using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使う場合

public class TakeDamage : MonoBehaviour
{
    public float knockbackForce = 5.0f;  // ノックバックの力
    public float knockbackDuration = 0.5f;   // ノックバックの持続時間

    PlayerAction playerAction;

    private Rigidbody rb;  // プレイヤーのRigidbody
    private Animator animator;  // プレイヤーのAnimator
    public TextMeshProUGUI gameOverText;  // TextMeshProのUIテキスト

    // Start is called before the first frame update
    void Start()
    {
        playerAction = GetComponent<PlayerAction>();

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();   // プレイヤーのAnimator
        gameOverText.gameObject.SetActive(false);   // ゲームオーバーテキストを非表示にする
    }

    public void Damage(Transform playerTransform)
    {
        // ダメージアニメーションの再生
        animator.SetTrigger("Damage");
        
        // ノックバック処理を呼び出す
        ApplyKnockback(playerTransform);
     }

    private void ApplyKnockback(Transform playerTransform)
    {
        Rigidbody rb = playerTransform.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // プレイヤーの進行方向を取得
            Vector3 knockbackDirection = -playerTransform.forward; // 進行方向の反対

            // ノックバックを適用
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            // ノックバック状態を設定
            playerTransform.GetComponent<PlayerController>().SetKnockbackState(true);
        }
    }

    public void GameOver()
    {
        // 死亡アニメーションの再生
        animator.SetBool("isDead", true);

        if(playerAction != null)
        {
            // プレイヤーの動きを停止する
            playerAction.DisableInput();
        }

        // ゲームオーバーのテキストを表示
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";
    }
}