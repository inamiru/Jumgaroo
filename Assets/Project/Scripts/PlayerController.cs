using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使う場合

public class PlayerController : MonoBehaviour
{
    public int maxCollisions = 3;  // 敵に接触できる最大回数
    private int collisionCount = 0;  // 現在の接触回数
    public float knockbackForce = 5.0f;  // ノックバックの力

    public TextMeshProUGUI gameOverText;  // TextMeshProのUIテキスト
    public PlayerAction playerAction; // プレイヤーの動き制御スクリプト

    private Rigidbody rb;  // プレイヤーのRigidbody
    private Animator animator;  // プレイヤーのAnimator
    private bool isDead = false;       // 死亡フラグ

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   // プレイヤーのRigidbody
        animator = GetComponent<Animator>();   // プレイヤーのAnimator
        gameOverText.gameObject.SetActive(false);   // ゲームオーバーテキストを非表示にする
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 敵と接触したかどうかをタグで判定
        if (collision.gameObject.CompareTag("Enemy") && !isDead)
        {
            // ダメージアニメーションの再生
            animator.SetTrigger("Damage");

            // ノックバック処理を呼び出す
            ApplyKnockback(collision);
            
            collisionCount++;

            // 接触回数が最大回数に達したらゲームオーバー
            if (collisionCount >= maxCollisions)
            {
                GameOver();
            }
        }
    }

    private void ApplyKnockback(Collision collision)
    {
        // 敵からプレイヤーへの方向を計算
        Vector3 knockbackDirection = transform.position - collision.transform.position;
        knockbackDirection.y = 0;  // Y軸のノックバックを制限する（垂直方向には飛ばないようにする）
        knockbackDirection.Normalize();  // 単位ベクトルに正規化

        // ノックバックの力をRigidbodyに加える
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    private void GameOver()
    {

        // 死亡フラグを立てる
        isDead = true;

        // 死亡アニメーションの再生
        animator.SetBool("isDead", true);

        // プレイヤーの動きを停止する
        playerAction.DisableInput();

        // ゲームオーバーのテキストを表示
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";
    }
}
