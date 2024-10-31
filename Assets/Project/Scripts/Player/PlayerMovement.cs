using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStates playerStates;      // プレイヤーのステータスを管理するScriptableObjectの参照
    public float currentSpeed = 0.0f;      // 現在の移動速度

    private Rigidbody rb;                  // プレイヤーのRigidbodyコンポーネントの参照
    private Animator animator;             // プレイヤーのアニメーターコンポーネントの参照
    private PlayerJump playerJump;

    private float dustEffectTimer = 0f;    // 土煙エフェクト生成のタイマー
    public float dustEffectInterval = 0.3f; // エフェクト生成の間隔（調整可能）
    private float dustEffectHeightOffset = 1.2f; // エフェクト位置のY座標オフセット
    private float dustEffectZOffset = -0.2f;     // エフェクト位置のZ座標オフセット

    private bool isMovable = true;         // 移動可能かどうかを示すフラグ

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
    }

    // プレイヤーの移動を完全に停止させるメソッド
    public void StopMovement()
    {
        currentSpeed = 0f;
        isMovable = false;  // 移動を停止する

        if (rb != null)
        {
            rb.velocity = Vector3.zero;  // Rigidbodyの速度をリセット
        }
    }

    // プレイヤーの移動を再開するメソッド
    public void ResumeMovement()
    {
        isMovable = true;
    }

    // FixedUpdateは物理演算用のメソッド
    void FixedUpdate()
    {
        // isMovableフラグを確認し、移動メソッドを呼び出し
        if (isMovable)
        {
            Move(true); // 実際のゲームロジックでcanMoveフラグを制御する場合は適宜変更
        }
    }

    // プレイヤーの移動処理を行うメソッド
    public void Move(bool canMove)
    {
        if (!canMove) return;

        // 速度の調整と制限
        currentSpeed = Mathf.Min(currentSpeed + playerStates.acceleration * Time.fixedDeltaTime, playerStates.maxSpeed);
        
        // プレイヤーの位置更新
        if (rb != null)
        {
            rb.MovePosition(transform.position + Vector3.right * currentSpeed * Time.fixedDeltaTime);
        }
        animator.SetFloat("Speed", currentSpeed);

        // 土煙エフェクトをジャンプ中でないときにのみ再生
        if (playerJump != null && !playerJump.IsJumping && currentSpeed > 0.1f)
        {
            dustEffectTimer += Time.fixedDeltaTime;
            if (dustEffectTimer >= dustEffectInterval)
            {
                ShowDustEffect();
                dustEffectTimer = 0f;
            }
        }
    }

    private void ShowDustEffect()
    {
        Vector3 effectPosition = transform.position + Vector3.up * dustEffectHeightOffset + transform.forward * dustEffectZOffset;
        EffectManager.Instance.PlayDustEffect(effectPosition);
    }
}
