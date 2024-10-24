using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour
{
    private PlayerJump playerJump;  // PlayerJumpの参照
    public float unlimitedJumpDuration = 5.0f;  // 無限ジャンプの継続時間（秒）

    // Start is called before the first frame update
    void Start()
    {
        playerJump = GetComponent<PlayerJump>();  // PlayerJump コンポーネントを取得
    }

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに接触した場合
        if (other.CompareTag("Player"))
        {
            // プレイヤーの無制限ジャンプを有効にして、指定時間後に解除
            PlayerJump playerJump = other.GetComponent<PlayerJump>();

            if (playerJump != null)
            {
            //    playerJump.EnableUnlimitedJumps(unlimitedJumpDuration);
            }

            // アイテムを削除
            Destroy(gameObject);
        }
    }
}
