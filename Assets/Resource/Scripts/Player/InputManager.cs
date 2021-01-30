﻿using UnityEditor.Animations;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player   player;
    [SerializeField] private Animator animator;

    void Start()
    {
        player   = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.getHit)
        {
            GoRight();
            if (player.LeftKey)
            {
                GoLeft();
            }

            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                animator.ResetTrigger("Run");
            }
        }

        if (player.Jump && !player.isJump && !player.isClimbing && !player.getHit)
        {
            Jump();
        }

        if (player.isClimbing)
        {
            Climb();
        }
    }

    private void Climb()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.localPosition += new Vector3(0, Time.deltaTime * player.runSpeed, 0);
            if (player.GetAnimator)
            {
                if (!player.GetBackgroundColor)
                {
                    animator.SetTrigger("Climb");
                }
                else
                {
                    animator.SetTrigger("Climb_C");
                }

            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.localPosition -= new Vector3(0, Time.deltaTime * player.runSpeed, 0);
            if (player.GetAnimator)
            {
                animator.SetTrigger("Climb");
            }
        }

        if (!Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            animator.ResetTrigger("Climb");
        }
    }

    private void GoRight()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition += new Vector3(Time.deltaTime * player.runSpeed, 0, 0);
            transform.localScale    =  new Vector3(1,                                1, 1);
            if (player.GetAnimator)
            {
                animator.SetTrigger("Run");
            }
        }
    }

    private void GoLeft()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localPosition -= new Vector3(Time.deltaTime * player.runSpeed, 0, 0);
            transform.localScale    =  new Vector3(-1,                               1, 1);
            if (player.GetAnimator)
            {
                animator.SetTrigger("Run");
            }
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.isJump = true;
            print("Jumping");
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2 * player.jumpForce);
            if (player.GetAnimator)
            {
                if (player.GetBackgroundColor)
                {

                }
                animator.SetTrigger("Jump");
            }

            if (player.GetAudio)
            {
                player.PlayJumpSFX();
            }
        }
    }
}