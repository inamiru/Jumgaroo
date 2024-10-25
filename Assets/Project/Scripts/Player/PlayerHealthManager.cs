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
        playerStates.InitializeHP();
        heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);
        animator = GetComponent<Animator>();
        gameOverController = FindObjectOfType<GameOverController>();

        previousHealth = playerStates.currentHitCount;
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        if (playerStates != null)
        {
            playerStates.TakeDamage(damage);
            UpdateHeartDisplay();
            CheckGameOver();
        }
        else
        {
            Debug.LogWarning("playerStates is null!");
        }
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
