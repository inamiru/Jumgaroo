using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public PlayerStates playerStates;  // ScriptableObject �̎Q��
    private Rigidbody rb;

    private bool isGrounded = false;    // �ڒn���Ă��邩�ǂ���
    public float rayDistance = 1.1f;    // ���C�̒���
    public LayerMask groundLayer;       // �n�ʃ��C���[

    private int jumpCount = 0;  // �W�����v�̉񐔂��J�E���g

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody��Animator�̎擾
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // ���C�L���X�g���g���Đڒn������s��
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayDistance, groundLayer);

        // �A�j���[�^�[�ɐڒn����̌��ʂ�n��
        animator.SetBool("isGrounded", isGrounded);
    }

    // �W�����v�̓��̓`�F�b�N
    public void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // �W�����v����
    public void Jump()
    {
        // �ڒn���Ă���ꍇ�̓W�����v�񐔂����Z�b�g
        if (isGrounded)
        {
            jumpCount = 0;  // �ڒn���ɃW�����v�񐔂����Z�b�g
        }

        // �W�����v�񐔂��ő�ɒB���Ă��Ȃ��ꍇ
        if (jumpCount < playerStates.maxJumps)
        {
            // Y�����̑��x�����Z�b�g���Ă��������ɃW�����v
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // �I�u�W�F�N�g�ɂԂ���Ƃ��̒���
            float jumpAdjustment = playerStates.jumpForce; // �����������ꍇ�͂��̒l��ύX
            rb.AddForce(Vector3.up * jumpAdjustment, ForceMode.Impulse);

            jumpCount++;
        }
    }

    // �f�o�b�O�p�F���C�L���X�g�̉���
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
}
