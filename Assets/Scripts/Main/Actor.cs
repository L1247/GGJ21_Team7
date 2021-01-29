using System;
using UnityEngine;

public class Actor : MonoBehaviour
{
#region Private Variables

    private bool _isGrounded;

    private Rigidbody2D _rigidbody2D;
    private Vector2     movement;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform GroundCheck;

#endregion

#region Public Methods

    public void AddMovementX(float move)
    {
        movement += Vector2.right * move;
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public bool IsOnLadder()
    {
        throw new NotImplementedException();
    }

    public void Jump(float jumpForce)
    {
        _rigidbody2D.AddForce(jumpForce * Vector2.up , ForceMode2D.Impulse);
    }

#endregion

#region Private Methods

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position
                                              , 0.15f , groundLayer);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        CheckGround();
    }

    private void HandleMovement()
    {
        if (movement.x != 0) MoveActor();
        else SetVelocityX(0);
        movement.x = 0;
    }


    private void MoveActor()
    {
        var movementX = movement.x;
        SetVelocityX(movementX);
    }

    private void SetVelocityX(float movementX)
    {
        var velocity    = _rigidbody2D.velocity;
        var x           = movementX * Time.fixedDeltaTime;
        var newVelocity = new Vector2(x , velocity.y);
        _rigidbody2D.velocity = newVelocity;
    }

    private void SetVelocityY(float movementY)
    {
        var velocity    = _rigidbody2D.velocity;
        var y           = movementY * Time.fixedDeltaTime;
        var newVelocity = new Vector2(velocity.x , y);
        _rigidbody2D.velocity = newVelocity;
    }

#endregion
}