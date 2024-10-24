using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TransitionsPlusDemos;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject の参照
    public GameOverController gameOverController;
     public HeartDisplayManager heartDisplayManager; // HeartDisplayManagerの参照

    private bool isDead = false;                // プレイヤーが死亡したかどうかのフラグ
    private Animator animator;  // プレイヤーのAnimator

    void Start()
    {
        playerStates.InitializeHP();  // ゲーム開始時にHPを初期化
        
        // ハートのUIを初期化するためにHeartDisplayManagerを呼び出す
        heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount); 

        animator = GetComponent<Animator>();   // プレイヤーのAnimator
        
        gameOverController = FindObjectOfType<GameOverController>(); // GameOverControllerを取得
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        if (playerStates != null)
        {
            // HPを減少させる
            playerStates.TakeDamage(damage);
            heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount); // ハートUIを更新

            // ハートが減ったエフェクトを再生
            EffectManager.instance.PlayHeartLostEffect(transform.position); // 現在の位置にエフェクトを表示

            // HPが0かどうか確認
            if (playerStates.IsDead() && !isDead)
            {
                isDead = true;  // 死亡フラグを立てる
                gameOverController.GameOver();  // HPが0ならゲームオーバー
            }
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
