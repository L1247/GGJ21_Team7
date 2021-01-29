using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;


    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Jump && !player.isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.isJump = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2*player.speed);
                if (player.GetAnimator)
                {
                    GetComponent<Animator>().SetTrigger("Jump");
                }
            }
        }

        if (player.GetLeftKey)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localPosition += new Vector3(-Time.deltaTime*player.runSpeed,0,0);
                transform.localScale = new Vector3(-1, 1, 1);
                if (player.GetAnimator)
                {
                    GetComponent<Animator>().SetBool("Run",true);
                }
                else
                {
                    GetComponent<Animator>().SetBool("Run",false);
                    GetComponent<Animator>().SetTrigger("Idle");
                }
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.localPosition += new Vector3(Time.deltaTime * player.speed,0, 0);
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Animator>().SetBool("Run",true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Run",false);
            GetComponent<Animator>().SetTrigger("Idle");
        }
    }
}
