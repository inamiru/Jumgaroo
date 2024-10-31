using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerStates playerStates;  // プレイヤーのステータスを管理するScriptableObjectの参照
    private Rigidbody rb;  // プレイヤーのRigidbodyコンポーネントの参照
    private bool isGrounded = false;    // プレイヤーが地面に接地しているかどうかのフラグ
    public float rayDistance = 1.1f;    // 地面を検出するRayの長さ
    public LayerMask groundLayer;       // 地面のレイヤーを指定する
    private int jumpCount = 0;  // ジャンプの回数をカウントする変数
    public bool IsJumping { get; private set; } // プレイヤーがジャンプ中かどうかを外部から取得可能なプロパティ
    private Animator animator;  // プレイヤーのアニメーターコンポーネントの参照
    private bool inputSuppressed = false;  // ジャンプ入力の抑制フラグ

    // Start is called before the first frame update
    void Start()
    {
        // RigidbodyとAnimatorコンポーネントの取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // 毎フレーム実行されるUpdateメソッド
    void FixedUpdate()
    {
        // Raycastで地面との接地を判定
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        // 接地している場合、ジャンプ回数をリセット
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // ジャンプ入力をチェック
        CheckJumpInput();
    }

    // ジャンプの入力チェックを行うメソッド
    public void CheckJumpInput()
    {
        if (!inputSuppressed && Input.GetKeyDown(KeyCode.Space))
        {
            SoundEffectManager.Instance.PlayerJumpSound();
            Jump();
        }
    }

    // ジャンプの処理を行うメソッド
    public void Jump()
    {
        IsJumping = true;

        if (jumpCount < playerStates.maxJumps)
        {
            // 現在のXとZの速度を保持
            float currentXVelocity = rb.velocity.x;
            float currentZVelocity = rb.velocity.z;

            // Y軸の速度をリセット
            rb.velocity = new Vector3(currentXVelocity, 0, currentZVelocity);
        
            // 上向きの力を加える
            rb.AddForce(Vector3.up * playerStates.jumpForce, ForceMode.Impulse);
            jumpCount++;

            SoundEffectManager.Instance.PlayerJumpSound(); // Play arrow key sound
            EffectManager.Instance.PlayJumpEffect(transform.position);
        }
    }

    // 他のオブジェクトと衝突した際に呼び出されるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("JumpBoost"))
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * playerStates.boostJumpForce, ForceMode.Impulse);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsJumping = false;
            jumpCount = 0;
        }
    }

    // 入力を無効化するメソッド（パネル表示中）
    public void DisableInput()
    {
        inputSuppressed = true;
    }

    // 入力を有効化するメソッド（パネル非表示後）
    public void EnableInput()
    {
        inputSuppressed = false;
    }

    // ジャンプバッファをクリアするメソッド
    public void ClearJumpBuffer()
    {
        inputSuppressed = true;
        Invoke(nameof(EnableInput), 0.1f); // 少し遅れて入力を再開
    }

    // デバッグ用にRayの描画を行う
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // Rayの色を赤に設定
        // プレイヤーの足元からRayを描画し、地面との距離を可視化
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}
