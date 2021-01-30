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
        if (!player.isClimbing && !player.getHit)
        {
            GoRight();
            if (player.LeftKey)
            {
                GoLeft();
            }
        }




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
                    animator.SetTrigger("Run");
                }
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.localPosition -= new Vector3(0, Time.deltaTime * player.runSpeed, 0);
                if (player.GetAnimator)
                {
                    animator.SetTrigger("Run");
                }
            }
        }
    }

    private void GoRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition += new Vector3(Time.deltaTime * player.runSpeed, 0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            if (player.GetAnimator)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
            }
        }
        else
        {
            if (player.GetAnimator)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            }
        }
    }

    private void GoLeft()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition -= new Vector3(Time.deltaTime * player.runSpeed, 0, 0);
            transform.localScale = new Vector3(-1, 1, 1);
            if (player.GetAnimator)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Run", true);
            }
        }
        else
        {
            if (player.GetAnimator)
            {
                animator.SetBool("Run", false);
                animator.SetBool("Idle", true);
            }
        }
    }

    private void Jump()
    {
        if (player.Jump && !player.isJump && !player.isClimbing && !player.getHit)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.isJump = true;
                print("Jumping");
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, player.jumpForce);
                if (player.GetAnimator)
                {
                    animator.SetBool("Idle", false);
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