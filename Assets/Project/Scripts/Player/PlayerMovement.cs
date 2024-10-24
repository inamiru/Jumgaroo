using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject �̎Q��
    public float currentSpeed = 0.0f; // ���݂̑��x

    private Rigidbody rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody��Animator�̎擾
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // �ړ������𖈃t���[���X�V
    public void Move(bool canMove)
    {
        if (!canMove) return;

        // ���X�ɉ����i���݂̑��x���ő呬�x�ɒB����܂Łj
        if (currentSpeed < playerStates.maxSpeed)
        {
            currentSpeed += playerStates.acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = playerStates.maxSpeed;
        }

        // Rigidbody���g���đO���Ɉړ�
        rb.MovePosition(transform.position + transform.forward * currentSpeed * Time.fixedDeltaTime);
        
        // �A�j���[�^�[�ɑ��x��n���āA�A�j���[�V�����𐧌�
        animator.SetFloat("Speed", currentSpeed);
    }
}
