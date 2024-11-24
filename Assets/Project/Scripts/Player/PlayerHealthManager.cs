using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlusDemos;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private PlayerStates playerStates;
    [SerializeField] private GameOverController gameOverController;
    [SerializeField] private HeartDisplayManager heartDisplayManager;
    [SerializeField] private Rigidbody playerRb;
    [SerializeField] private Renderer playerRenderer; // プレイヤーの見た目を操作するためのRenderer
    [SerializeField] private Collider playerCollider; // プレイヤーのコライダー
    
    private bool isDead = false;
    private Animator animator;


    void Start()
    {
        playerStates.InitializeHP();
        heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);
        animator = GetComponent<Animator>();
    }

    public void ApplyDamage(int damage, Vector3 enemyPosition, string sourceTag)
    {
        if (sourceTag == "DamageArea")
        {
            // ダメージエリア：ダメージのみ適用、ノックバックと無敵なし
            playerStates.TakeDamage(damage);
            heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);
            CheckGameOver();
            return;
        }

        if (sourceTag == "Enemy")
        {
            if (playerStates.isInvincible)
            {
                Debug.Log("無敵状態のためダメージ無効");
                return;
            }

            // ダメージ処理
            playerStates.TakeDamage(damage);
            heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);

            // ノックバック処理
            Vector3 knockbackDirection = (transform.position - enemyPosition).normalized;
            ApplyKnockback(knockbackDirection);

            // 無敵状態を開始
            StartCoroutine(InvincibleCoroutine());

            // ダメージアニメーション
            PlayDamageAnimation();

            // ゲームオーバー判定
            CheckGameOver();
        }
    }

    private void ApplyKnockback(Vector3 direction)
    {
        if (playerStates == null || playerRb == null) return;

        Vector3 knockback = direction.normalized * playerStates.knockbackForce;
        playerRb.AddForce(knockback, ForceMode.Impulse);
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

    private IEnumerator InvincibleCoroutine()
    {
        playerStates.isInvincible = true;
        float elapsedTime = 0f;

        // エネミーとの衝突を無視
        IgnoreEnemyCollision(true);

        while (elapsedTime < playerStates.invincibilityDuration)
        {
            // 点滅処理
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.2f;
        }

        // 無敵終了
        playerRenderer.enabled = true;
        playerStates.isInvincible = false;

        // エネミーとの衝突を再び有効化
        IgnoreEnemyCollision(false);
    }

    private void IgnoreEnemyCollision(bool ignore)
    {
        // すべてのエネミーと衝突を無視または再有効化
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Collider enemyCollider = enemy.GetComponent<Collider>();
            if (enemyCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, enemyCollider, ignore);
            }
        }
    }
}
