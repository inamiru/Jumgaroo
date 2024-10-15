using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour
{
     public float unlimitedJumpDuration = 5.0f;  // 無限ジャンプの継続時間（秒）

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに接触した場合
        if (other.CompareTag("Player"))
        {
            // プレイヤーの無制限ジャンプを有効にして、指定時間後に解除
            PlayerAction playerAction = other.GetComponent<PlayerAction>();

            if (playerAction != null)
            {
                playerAction.EnableUnlimitedJumps(unlimitedJumpDuration);
            }

            // アイテムを削除
            Destroy(gameObject);
        }
    }
}
