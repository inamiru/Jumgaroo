using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject の参照
    public float currentSpeed = 0.0f; // 現在の速度

    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // RigidbodyとAnimatorの取得
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // 移動処理を毎フレーム更新
    public void Move(bool canMove)
    {
        if (!canMove) return;

        // 徐々に加速（現在の速度が最大速度に達するまで）
        if (currentSpeed < playerStates.maxSpeed)
        {
            currentSpeed += playerStates.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = playerStates.maxSpeed;
        }

        // Rigidbodyを使って前方に移動
        rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.fixedDeltaTime);
        
        // アニメーターに速度を渡して、アニメーションを制御
        animator.SetFloat("Speed", currentSpeed);
    }
}
