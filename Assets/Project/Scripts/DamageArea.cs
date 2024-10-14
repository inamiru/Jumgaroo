using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public int damageIncrement = 1; // 増加する接触回数
    private bool isPlayerInside = false; // プレイヤーがエリア内にいるかどうかを示すフラグ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlayerInside) // プレイヤーと接触し、まだ入っていない場合
        {
            isPlayerInside = true; // プレイヤーがエリア内にいると設定
            // プレイヤーのDamageManagerを取得
            PlayerController playerController = other.GetComponent<PlayerController>();
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            
            if (playerController != null)
            {
                playerRespawn.Respawn(); // リスポーンを呼び出す
                playerController.IncreaseHitCount(damageIncrement); // 接触回数を増やすメソッドを呼び出す
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // プレイヤーがエリアから出た場合
        {
            isPlayerInside = false; // プレイヤーがエリア内にいないと設定
        }
    }

}
