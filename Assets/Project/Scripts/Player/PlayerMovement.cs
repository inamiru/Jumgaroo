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

    // Start is called before the first frame update
    void Start()
    {
        // RigidbodyとAnimatorコンポーネントの取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerJump = GetComponent<PlayerJump>();
    }

    // プレイヤーの移動処理を行うメソッド
    // canMoveがtrueの場合に移動処理を実行する
    public void Move(bool canMove)
    {
        if (!canMove) return;

        if (currentSpeed < playerStates.maxSpeed)
        {
            currentSpeed += playerStates.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = playerStates.maxSpeed;
        }

        rb.MovePosition(transform.position + Vector3.right * currentSpeed * Time.fixedDeltaTime);
        animator.SetFloat("Speed", currentSpeed);

        // 土煙エフェクトをジャンプ中でないときにのみ再生
        if (!playerJump.IsJumping && currentSpeed > 0.1f)
        {
            dustEffectTimer += Time.deltaTime;
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
