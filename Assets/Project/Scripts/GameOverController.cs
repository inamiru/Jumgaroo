using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使う場合

public class PlayerController : MonoBehaviour
{
    public int maxCollisions = 3;  // 敵に接触できる最大回数
    private int collisionCount = 0;  // 現在の接触回数

    public TextMeshProUGUI gameOverText;  // TextMeshProのUIテキスト
    public PlayerAction playerAction; // プレイヤーの動き制御スクリプト

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオーバーテキストを非表示にする
        gameOverText.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 敵と接触したかどうかをタグで判定
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collisionCount++;

            // 接触回数が最大回数に達したらゲームオーバー
            if (collisionCount >= maxCollisions)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        // プレイヤーの動きを停止する
        playerAction.DisableInput();

        // ゲームオーバーのテキストを表示
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";

        // 他にゲームオーバー時の処理をここに追加 (リスタートボタンを表示するなど)
    }
}
