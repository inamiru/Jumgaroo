using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject の参照
    private GameOverController gameOverController;

    private bool isDead = false;                // プレイヤーが死亡したかどうかのフラグ
    public Image[] heartImages; // ハートのUI（Imageコンポーネント）を格納する配列

    private Animator animator;  // プレイヤーのAnimator

    void Start()
    {
        playerStates.InitializeHP();  // ゲーム開始時にHPを初期化
        UpdateHeartUI(); // ゲーム開始時にハートUIを初期化

        animator = GetComponent<Animator>();   // プレイヤーのAnimator
        gameOverController = gameObject.GetComponent<GameOverController>();
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        // HPを減少させる
        playerStates.TakeDamage(damage);
        UpdateHeartUI(); // ハートUIを更新

        Debug.Log("プレイヤーは " + damage + " ダメージを受けた。残りHP: " + playerStates.currentHitCount);

        // HPが0かどうか確認
        if (playerStates.IsDead() && !isDead)
        {
            isDead = true; // 死亡フラグを立てる
            gameOverController.GameOver();  // HPが0ならゲームオーバー
        }
    }

    // ハートUIを更新するメソッド
    private void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < playerStates.currentHitCount)
            {
                heartImages[i].enabled = true; // ライフが残っている場合、ハートを表示
            }
            else
            {
                heartImages[i].enabled = false; // ライフが減った場合、ハートを非表示
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
