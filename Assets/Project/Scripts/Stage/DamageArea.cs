using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damageAmount = 1; // 増加する接触回数

    // ScriptableObject のインスタンス
    public PlayerStates playerStates;
    private GameOverController gameOverController;

    void Start()
    {
        gameOverController = GetComponent<GameOverController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーのHealthManagerを取得
            PlayerHealthManager playerHealth = other.gameObject.GetComponent<PlayerHealthManager>();
               
            if (playerHealth != null)
            {
                // ダメージを与える
                playerHealth.TakeDamage(damageAmount);

                // プレイヤーのリスポーン機能を呼び出す
                PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();

                if (playerRespawn != null)
                {
                    playerRespawn.Respawn();
                }
            }
        }
    }
}
