using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damageAmount = 1; // 増加する接触回数

    // PlayerRespawnを格納する変数
    private PlayerRespawn playerRespawn;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーのHealthManagerを取得
            PlayerHealthManager playerHealth = other.gameObject.GetComponent<PlayerHealthManager>();

            // プレイヤーのPlayerRespawnスクリプトを取得
            playerRespawn = other.gameObject.GetComponent<PlayerRespawn>();

            if (playerHealth != null)
            {
                // ダメージを与える（タグ情報を追加）
                playerHealth.ApplyDamage(damageAmount, transform.position, "DamageArea");
            }

            if (playerRespawn != null)
            {
                // ダメージを受けた後にリスポーンを呼び出す
                playerRespawn.Respawn();
            }
        }
    }
}
