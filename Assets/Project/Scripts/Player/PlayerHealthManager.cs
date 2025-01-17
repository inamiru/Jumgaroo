using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlusDemos;

public class PlayerHealthManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerStates playerStates;        // プレイヤーの状態を管理する ScriptableObject
    [SerializeField] private GameOverController gameOverController; // ゲームオーバー時の処理を管理
    [SerializeField] private HeartDisplayManager heartDisplayManager; // ハートUIの更新を管理
    [SerializeField] private Rigidbody playerRb;              // プレイヤーの物理挙動
    [SerializeField] private Renderer playerRenderer;         // プレイヤーの見た目操作
    [SerializeField] private Collider playerCollider;         // プレイヤーのコライダー

  
    public bool isDead { get; private set; }  // 死亡状態かどうか

    private Animator animator;                                // プレイヤーのアニメーション制御

    void Start()
    {
        // プレイヤーの体力を初期化し、UIを更新
        playerStates.InitializeHP();
        heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);

        // アニメーターを取得
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// プレイヤーにダメージを適用する
    /// </summary>
    /// <param name="damage">受けるダメージ量</param>
    /// <param name="sourceTag">ダメージソースのタグ（Enemy や DamageArea）</param>
    public void ApplyDamage(int damage, string sourceTag)
    {
        if (sourceTag == "DamageArea")
        {
            // ダメージエリアの場合：ノックバックや無敵なしでダメージのみ適用
            playerStates.TakeDamage(damage);
            heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);
            CheckGameOver(); // 死亡判定を実行
            return;
        }

        if (sourceTag == "Enemy")
        {
            // 無敵状態の場合はダメージを無効化
            if (playerStates.isInvincible)
            {
                Debug.Log("無敵状態のためダメージ無効");
                return;
            }

            // ダメージ適用
            playerStates.TakeDamage(damage);
            heartDisplayManager.UpdateHeartUI(playerStates.currentHitCount);

            // 死亡しているかをチェック
            if (playerStates.IsDead())
            {
                CheckGameOver(); // 死亡判定を実行
                return; // ノックバックをスキップ
            }

            // ノックバック適用
            ApplyKnockback();

            // 無敵状態を開始
            StartCoroutine(InvincibleCoroutine());

            // ダメージアニメーションを再生
            PlayDamageAnimation();

            // 死亡判定を再確認
            CheckGameOver();
        }
    }

    /// <summary>
    /// ノックバック処理を適用（常にプレイヤーの後ろ方向）
    /// </summary>
    private void ApplyKnockback()
    {
        if (playerStates == null || playerRb == null) return;

        // プレイヤーの後ろ方向を計算
        Vector3 knockbackDirection = -transform.forward;

        // ノックバックの力を適用
        Vector3 knockback = knockbackDirection.normalized * playerStates.knockbackForce;
        playerRb.AddForce(knockback, ForceMode.Impulse);
    }

    /// <summary>
    /// プレイヤーの死亡状態をチェック
    /// </summary>
    private void CheckGameOver()
    {
        if (playerStates.IsDead() && !isDead)
        {
            isDead = true; // 死亡状態をフラグで記録
            gameOverController.GameOver(); // ゲームオーバー処理を呼び出し
        }
    }

    /// <summary>
    /// ダメージを受けた際のアニメーションを再生
    /// </summary>
    public void PlayDamageAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Damage"); // アニメーションにトリガーを送信
        }
    }

    /// <summary>
    /// 無敵状態を管理するコルーチン
    /// </summary>
    /// <returns>コルーチンの状態</returns>
    private IEnumerator InvincibleCoroutine()
    {
        playerStates.isInvincible = true; // 無敵状態を有効化
        float elapsedTime = 0f;

        // エネミーとの衝突を無視
        IgnoreEnemyCollision(true);

        while (elapsedTime < playerStates.invincibilityDuration)
        {
            // プレイヤーを点滅させる（視覚的な無敵効果）
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(0.2f); // 点滅間隔
            elapsedTime += 0.2f;
        }

        // 無敵状態を終了
        playerRenderer.enabled = true;
        playerStates.isInvincible = false;

        // エネミーとの衝突を再び有効化
        IgnoreEnemyCollision(false);
    }

    /// <summary>
    /// エネミーとの衝突を無効化または再有効化
    /// </summary>
    /// <param name="ignore">衝突を無視するかどうか</param>
    private void IgnoreEnemyCollision(bool ignore)
    {
        // エネミーをすべて取得
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (var enemy in enemies)
        {
            Collider enemyCollider = enemy.GetComponent<Collider>();

            if (enemyCollider != null)
            {
                // プレイヤーとエネミーの衝突を設定
                Physics.IgnoreCollision(playerCollider, enemyCollider, ignore);
            }
        }
    }
}
