using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがダメージエリアに入った際の処理を管理するクラス
/// </summary>
public class DamageArea : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1; // 与えるダメージ量

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーとの接触を確認
        if (other.CompareTag("Player"))
        {
            // 必要なコンポーネントを取得
            PlayerHealthManager playerHealth = other.GetComponent<PlayerHealthManager>();
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

            if (playerHealth == null || playerRespawn == null)
            {
                Debug.LogWarning($"Player is missing required components: " +
                                 $"{(playerHealth == null ? "PlayerHealthManager " : "")}" +
                                 $"{(playerRespawn == null ? "PlayerRespawn" : "")}");
                return;
            }

            // ダメージを与える
            playerHealth.ApplyDamage(damageAmount, "DamageArea");

            // リスポーン処理
            StartCoroutine(RespawnAfterDelay(playerRespawn, 0.1f)); // 0.5秒の遅延を追加
        }
    }

    /// <summary>
    /// リスポーンを指定時間後に実行
    /// </summary>
    private IEnumerator RespawnAfterDelay(PlayerRespawn playerRespawn, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerRespawn.Respawn();
    }
}
