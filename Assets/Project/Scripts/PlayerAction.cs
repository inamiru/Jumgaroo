using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float acceleration = 1.0f; // 加速度
    public float maxSpeed = 10.0f;    // 最大速度
    private float currentSpeed = 0.0f; // 現在の速度

    public float jumpForce = 5.0f;      // ジャンプ力
    public float boostJumpForce = 10.0f;     // ジャンプブーストのジャンプ力
    public float rayDistance = 1.1f;    // レイの長さ（キャラクターの足元から少し下まで）
    public LayerMask groundLayer;       // 地面レイヤー
    private bool isGrounded = false;    // 接地しているかどうか
    private int jumpCount = 0;          // ジャンプの回数
    public int maxJumps = 2;            // 最大ジャンプ回数（二段ジャンプを許可するため2に設定）

    private bool canMove = false;    // プレイヤーの入力を受け付けるかどうかを制御するフラグ

    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbodyコンポーネントを取得
        rb = GetComponent<Rigidbody>();
        //アニメーター取得
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // アニメーターのパラメータを設定
        animator.SetBool("isGrounded", isGrounded);

        // レイキャストを使って接地判定を行う
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        if (!canMove)
        {
            // 入力を受け付けない場合、プレイヤーを動かさない
            return;
        }

        // 接地している場合、ジャンプ回数をリセット
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // スペースキーが押され、ジャンプ回数が最大値未満の場合ジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
        }

        // 徐々に加速（現在の速度が最大速度に達するまで）
        if(currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            // 最大速度に達したらそれ以上加速しない
            currentSpeed = maxSpeed;
        }

        // Rigidbodyを使って前方に移動
        rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.fixedDeltaTime);
        // アニメーションのパラメータに速度を渡す
        animator.SetFloat("Speed", currentSpeed);
    }

    void Jump()
    {
        // 現在の垂直速度をリセットし、上方向にジャンプ
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // 上方向の速度をリセット
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // ジャンプアニメーションのトリガーをセット
        animator.SetTrigger("Jump");

        // ジャンプ回数をカウント
        jumpCount++;
    }

    // 外部から呼び出して入力を有効にするメソッド
    public void EnableInput()
    {
        canMove = true;
    }

    public void DisableInput()
    {
        canMove = false;  // プレイヤーの入力を無効にする
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ジャンプブーストの面に接触した場合
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            // 既存のY方向の速度をリセット
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // ブーストジャンプ力を瞬間的に加える
            rb.AddForce(Vector3.up * boostJumpForce, ForceMode.Impulse);
        }
    }

    // デバッグ用：レイキャストの可視化
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}
