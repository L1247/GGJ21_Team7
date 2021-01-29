using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }


    public void Jump(float jumpForce)
    {
        _rigidbody2D.AddForce(jumpForce * Vector2.up , ForceMode2D.Impulse);
    }
}
