using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Animator animator;

    void Start()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        GoLeft();

        GoRight();

        Climb();

    }

    private void Climb()
    {
        if (player.isClimbing)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.localPosition += new Vector3(0, Time.deltaTime * player.runSpeed, 0);
                if (player.GetAnimator)
                {
                    animator.SetBool("Climb", true);
                }
            }
            else
            {
                if (player.GetAnimator)
                {
                    animator.SetBool("Climb", false);
                    animator.SetTrigger("Idle");
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.localPosition -= new Vector3(0, Time.deltaTime * player.runSpeed, 0);
                if (player.GetAnimator)
                {
                    animator.SetBool("Climb", true);
                }
            }
            else
            {
                if (player.GetAnimator)
                {
                    animator.SetBool("Climb", false);
                    animator.SetTrigger("Idle");
                }
            }
        }
    }

    private void GoRight()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !player.isClimbing)
        {
            transform.localPosition += new Vector3(Time.deltaTime * player.runSpeed, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            if (player.GetAnimator)
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            if (player.GetAnimator)
            {
                animator.SetBool("Run", false);
                animator.SetTrigger("Idle");
            }
        }
    }

    private void GoLeft()
    {
        if (player.LeftKey)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && !player.isClimbing)
            {
                transform.localPosition -= new Vector3(Time.deltaTime * player.runSpeed, 0, 0);
                transform.localScale = new Vector3(-1, 1, 1);
                if (player.GetAnimator)
                {
                    animator.SetBool("Run", true);
                }
            }
            else
            {
                if (player.GetAnimator)
                {
                    animator.SetBool("Run", false);
                    animator.SetTrigger("Idle");
                }
            }
        }
    }

    private void Jump()
    {
        if (player.Jump && !player.isJump && !player.isClimbing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.isJump = true;
                print("Jumping");
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2 * player.jumpForce);
                if (player.GetAnimator)
                {
                    animator.SetTrigger("Jump");
                }

                if (player.GetAudio)
                {
                    player.PlayJumpSFX();
                }
            }
        }
    }
}
