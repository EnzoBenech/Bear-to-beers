using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 15f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        // Check if we have landed to reset the jumping state
        if (isGrounded)
        {
            animator.SetBool("Jump", false);
        }
    }

    // Receive the inputs for our InputManager.cs
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        // Set animator parameters for movement
        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveZ", input.y);

        // Determine which animation to play based on movement direction
        if (input != Vector2.zero)
        {
            if (input.y > 0)
            {
                animator.Play("RunForward");
            }
            else if (input.y < 0)
            {
                animator.Play("RunBackward");
            }
            else if (input.x > 0)
            {
                animator.Play("RunLeft"); // Corrected to RunLeft
            }
            else if (input.x < 0)
            {
                animator.Play("RunRight"); // Corrected to RunRight
            }
        }
        else
        {
            // Stop running animation if there's no input
            animator.Play("Idle");
        }

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0f)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

            // Set IsJumping parameter to true to trigger jump animation
            animator.SetBool("IsJumping", true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isGrounded)
        {
            // Reset the jump animation when grounded
            animator.ResetTrigger("Jump");
        }
    }
}
