using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlusDemos;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private PlayerStates playerStates;
    [SerializeField] private GameOverController gameOverController;
    [SerializeField] private HeartDisplayManager heartDisplayManager;

    private bool isDead = false;
    private Animator animator;
    private int previousHealth;

    void Start()
    {
        if (playerStates == null || heartDisplayManager == null || gameOverController == null)
        {
            Debug.LogError("PlayerHealthManager: 必要なコンポーネントが設定されていません");
            return;
        }

        playerStates.InitializeHP();
        heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);
        animator = GetComponent<Animator>();

        previousHealth = playerStates.currentHitCount;
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        playerStates.TakeDamage(damage);
        heartDisplayManager.PlayHeartLostEffect(previousHealth - playerStates.currentHitCount); // ここでエフェクトを再生
        CheckGameOver();
    }

    // ダメージを受けた後のUIを更新
    private void UpdateHeartDisplay()
    {
        heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);

        if (playerStates.currentHitCount < previousHealth)
        {
            heartDisplayManager.PlayHeartLostEffect(previousHealth - playerStates.currentHitCount);
        }

        previousHealth = playerStates.currentHitCount;
    }

    private void CheckGameOver()
    {
        if (playerStates.IsDead() && !isDead)
        {
            isDead = true;
            gameOverController.GameOver();
        }
    }

    public void PlayDamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }
    }
}
