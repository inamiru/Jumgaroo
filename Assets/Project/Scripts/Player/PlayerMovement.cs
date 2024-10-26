using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStates playerStates;  // プレイヤーのステータスを管理するScriptableObjectの参照
    public float currentSpeed = 0.0f;  // 現在の移動速度

    private Rigidbody rb;              // プレイヤーのRigidbodyコンポーネントの参照
    private Animator animator;         // プレイヤーのアニメーターコンポーネントの参照

    // Start is called before the first frame update
    void Start()
    {
        // RigidbodyとAnimatorコンポーネントの取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // プレイヤーの移動処理を行うメソッド
    // canMoveがtrueの場合に移動処理を実行する
    public void Move(bool canMove)
    {
        if (!canMove) return;  // canMoveがfalseなら処理を中断

        // プレイヤーの速度を徐々に加速させ、最大速度に達するまでcurrentSpeedを増加させる
        if (currentSpeed < playerStates.maxSpeed)
        {
            currentSpeed += playerStates.acceleration * Time.deltaTime;  // 加速を適用
        }
        else
        {
            currentSpeed = playerStates.maxSpeed;  // 最大速度に達したらそれ以上加速しない
        }

        // Rigidbodyを使用してプレイヤーを前方に移動させる
        rb.MovePosition(transform.position + Vector3.right * currentSpeed * Time.fixedDeltaTime);
        
        // アニメーターに現在の移動速度を反映し、移動アニメーションを再生
        animator.SetFloat("Speed", currentSpeed);
    }
}
