using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    Vector3 movedir = Vector3.zero;

    public float speedZ;
    public float acceleratorZ;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (characterController.isGrounded)
            {
                animator.SetBool("Run",false);
                Jump();
            }
        }
        
        Move();

        movedir.y -= 20f * Time.deltaTime;

        if (characterController.isGrounded)
        {
            movedir.y = 0;
        }
    }

    void Jump()
    {
        animator.SetTrigger("Jump");
        movedir.y = 15f;
    }

    void Move()
    {
        animator.SetBool("Run", true);
        movedir.z = Mathf.Clamp(movedir.z + (acceleratorZ * Time.deltaTime), 0, speedZ);
        Vector3 globaldir = transform.TransformDirection(movedir);
        characterController.Move(globaldir * Time.deltaTime);

    }
}
