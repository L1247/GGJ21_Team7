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
        if (movement != Vector2.zero)
        {
            MoveActor();
            movement = Vector2.zero;
        }
    }

    private void MoveActor()
    {
        Debug.Log($"{movement}");
        var velocity = _rigidbody2D.velocity;
        _rigidbody2D.velocity += new Vector2(movement.x * Time.fixedDeltaTime
                                             , velocity.y);
    }

#endregion
}