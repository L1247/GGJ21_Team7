using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float timer = 0;
    public float speed;
    public float runSpeed;


    [SerializeField] private bool getAnimator;
    public bool GetAnimator
    {
        get => getAnimator;
        set => getAnimator = true;
    }

    [SerializeField] bool getLeftKey;
    public bool GetLeftKey
    {
        get => getLeftKey;
        set => getLeftKey = true;
    }

    [SerializeField] private bool getJump;
    public bool Jump
    {
        get => getJump;
        set => getJump = true;
    }
    public bool isJump;



    void Start()
    {
        getAnimator = false;
        getLeftKey = false;
        getJump = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJump)
        {
            timer += Time.deltaTime;
            if (timer>=1)
            {
                timer = 0;
                isJump = false;
            }
        }
    }
}