using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "PlayerStatesSO", menuName = "ScriptableObjects/PlayerStatesScriptableObject", order = 1)]
public class PlayerStates : ScriptableObject
{
    [Header("Health Settings")]
    public int maxHits = 3;  // 最大ヒット回数
    public int currentHitCount;   // 現在のヒット回数
    public bool isInvincible = false;  // 無敵状態かどうか
    public float invincibilityDuration = 2.0f;  // 無敵状態の持続時間

    [Header("Knockback Settings")]
    public float knockbackForce = 5f;        // ノックバックの力
    public float knockbackDuration = 0.5f;  // ノックバックの持続時間

    [Header("Movement Settings")]
    public float acceleration = 1.0f;      // 加速度
    public float initialSpeed = 1.0f;      // 初期速度
    public float maxSpeed = 10.0f;         // 最大速度
    public float jumpForce = 5.0f;         // ジャンプ力
    public int maxJumps = 2;               // 最大ジャンプ回数
    public float boostJumpForce = 10.0f;  // ジャンプブースト力
    public float forwardForce = 5f;        // 無限ジャンプ時の前方力

    // 初期化メソッド
    public void InitializeHP()
    {
        currentHitCount = maxHits;
    }

    // ダメージを受ける処理
    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("無敵状態のためダメージ無効");
            return;
        }

        currentHitCount -= damage;
        currentHitCount = Mathf.Clamp(currentHitCount, 0, maxHits);

    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    // 無敵状態を設定
    public void SetInvincible(bool state)
    {
        isInvincible = state;
    }

    // HPが0かどうか確認する
    public bool IsDead()
    {
        return currentHitCount <= 0;
    }
}