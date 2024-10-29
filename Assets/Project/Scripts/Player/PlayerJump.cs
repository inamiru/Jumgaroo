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

    // Start is called before the first frame update
    void Start()
    {
        // RigidbodyとAnimatorコンポーネントの取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // 毎フレーム実行されるUpdateメソッド
    void Update()
    {
        // Raycastを使用して、プレイヤーが地面に接地しているかどうかをチェック
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        // アニメーターにisGroundedの状態を反映
        animator.SetBool("isGrounded", isGrounded);
    }

    // ジャンプの入力チェックを行うメソッド
    public void CheckJumpInput()
    {
        // スペースキーが押されたらJumpメソッドを呼び出す
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // ジャンプの処理を行うメソッド
    public void Jump()
    {
        IsJumping = true;

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (jumpCount < playerStates.maxJumps)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * playerStates.jumpForce, ForceMode.Impulse);
            jumpCount++;

            // ジャンプエフェクトを再生する
            EffectManager.Instance.PlayJumpEffect(transform.position);
        }
    }

    // 他のオブジェクトと衝突した際に呼び出されるメソッド
    private void OnCollisionEnter(Collision collision)
    {
        // "JumpBoost"レイヤーに属するオブジェクトと衝突した場合
        if (collision.gameObject.layer == LayerMask.NameToLayer("JumpBoost"))
        {
            // Y軸の速度をリセットして、ジャンプブーストを追加
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * playerStates.boostJumpForce, ForceMode.Impulse);  // ブーストジャンプ
        }

        // "Ground"レイヤーに属するオブジェクトと衝突した場合
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            IsJumping = false;  // ジャンプ終了
            jumpCount = 0;  // ジャンプ回数をリセット
        }
    }

    // デバッグ用にRayの描画を行う
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;  // Rayの色を赤に設定
        // プレイヤーの足元からRayを描画し、地面との距離を可視化
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}
