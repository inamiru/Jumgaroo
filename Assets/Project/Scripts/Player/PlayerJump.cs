using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject の参照
    private Rigidbody rb;

    private bool isGrounded = false;    // 接地しているかどうか
    public float rayDistance = 1.1f;    // レイの長さ
    public LayerMask groundLayer;       // 地面レイヤー

    private int jumpCount = 0;  // ジャンプの回数をカウント

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // RigidbodyとAnimatorの取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // レイキャストを使って接地判定を行う
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        // アニメーターに接地判定の結果を渡す
        animator.SetBool("isGrounded", isGrounded);
    }

    // ジャンプの入力チェック
    public void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // ジャンプ処理
    public void Jump()
    {
        // 接地している場合はジャンプ回数をリセット
        if (isGrounded)
        {
            jumpCount = 0;  // 接地時にジャンプ回数をリセット
        }

        // ジャンプ回数が最大に達していない場合
        if (jumpCount < playerStates.maxJumps)
        {
            // Y方向の速度をリセットしてから上方向にジャンプ
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // オブジェクトにぶつかるときの調整
            float jumpAdjustment = playerStates.jumpForce; // 調整したい場合はこの値を変更
            rb.AddForce(Vector3.up * jumpAdjustment, ForceMode.Impulse);

            jumpCount++;
        }
    }

    // デバッグ用：レイキャストの可視化
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}
