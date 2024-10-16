using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D.TopDownShooter;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damageAmount = 1;  // 与えるダメージの量

    // ScriptableObject のインスタンス
    public PlayerStates playerStates;

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに接触したか確認
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーのRigidbodyを取得
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

                // プレイヤーのHealthManagerを取得
                PlayerHealthManager playerHealth = collision.gameObject.GetComponent<PlayerHealthManager>();

                    // プレイヤーの後ろ方向を計算
                    Vector3 knockbackDirection = collision.transform.TransformDirection(Vector3.back); // プレイヤーの後ろ方向

                    // ダメージアニメーションを再生
                    playerHealth.PlayDamageAnimation();

                    // ダメージを与える
                    playerHealth.TakeDamage(damageAmount);

                    // ノックバック処理
                    Knockback(playerRb, knockbackDirection);
        }
    }
    
    // ノックバック処理
    private void Knockback(Rigidbody playerRb, Vector3 direction)
    {
        // ScriptableObject で定義されたノックバックの力を使用
        Vector3 knockback = direction.normalized * playerStates.knockbackForce; // 正規化して力を適用
        playerRb.AddForce(knockback, ForceMode.Impulse);  // 力を瞬時に加える
    }
}
