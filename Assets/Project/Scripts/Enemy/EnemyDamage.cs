using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーと接触した際にダメージを与えるクラス
/// </summary>
public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;  // 与えるダメージ量

    private void OnCollisionEnter(Collision collision)
    {
        // プレイヤーと接触した場合の処理
        if (collision.gameObject.CompareTag("Player"))
        {
            // 必要なコンポーネントを取得
            PlayerHealthManager playerHealth = collision.gameObject.GetComponent<PlayerHealthManager>();
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerHealth == null || playerRb == null)
            {
                Debug.LogWarning("Player object is missing required components (PlayerHealthManager or Rigidbody).");
                return;
            }

            // ダメージ適用
            playerHealth.ApplyDamage(damageAmount, "Enemy");
        }
    }
}
