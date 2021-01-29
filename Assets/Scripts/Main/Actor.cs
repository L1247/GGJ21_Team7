using UnityEngine;

public class Actor : MonoBehaviour
{
#region Private Variables

    private Rigidbody2D _rigidbody2D;
    private Vector2     movement;

#endregion

#region Public Methods

    public void AddMovement(Vector2 dir)
    {
        movement += dir;
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

    private void FixedUpdate()
    {
        if (movement.x != 0) MoveActor();
        else SetVelocity(0);
        movement.x = 0;
    }

    private void MoveActor()
    {
        var movementX = movement.x;
        SetVelocity(movementX);
    }

    private void SetVelocity(float movementX)
    {
        var velocity    = _rigidbody2D.velocity;
        var x           = movementX * Time.fixedDeltaTime;
        var newVelocity = new Vector2(x , velocity.y);
        _rigidbody2D.velocity = newVelocity;
    }

#endregion
}