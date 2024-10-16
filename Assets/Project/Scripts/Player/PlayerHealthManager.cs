using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject の参照
    private GameOverController gameOverController;

    private bool isDead = false;                // プレイヤーが死亡したかどうかのフラグ

    private Animator animator;  // プレイヤーのAnimator

    void Start()
    {
        playerStates.InitializeHP();  // ゲーム開始時にHPを初期化

        animator = GetComponent<Animator>();   // プレイヤーのAnimator
        gameOverController = gameObject.GetComponent<GameOverController>();
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        // HPを減少させる
        playerStates.TakeDamage(damage);
        Debug.Log("プレイヤーは " + damage + " ダメージを受けた。残りHP: " + playerStates.currentHitCount);

        // HPが0かどうか確認
        if (playerStates.IsDead() && !isDead)
        {
            isDead = true; // 死亡フラグを立てる
            gameOverController.GameOver();  // HPが0ならゲームオーバー
        }
    }

    // ダメージアニメーションを再生する
    public void PlayDamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Damage"); 
        }
    }
}
