using UnityEngine;

public class Actor : MonoBehaviour
{
#region Private Variables

    private bool _isGrounded;

    private bool _isOnLadder;

    private float _defaultGravityScale;

    private Rigidbody2D _rigidbody2D;
    private Vector2     movement;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private LayerMask ladderLayer;

    [SerializeField]
    private Transform GroundCheck;

    [SerializeField]
    private Transform LadderCheck;

#endregion

#region Public Methods

    public void AddMovementX(float x)
    {
        movement += Vector2.right * x;
    }

    public void AddMovementY(float y)
    {
        movement += Vector2.up * y;
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }

    public bool IsOnLadder()
    {
        return _isOnLadder;
    }

    public void Jump(float jumpForce)
    {
        _rigidbody2D.AddForce(jumpForce * Vector2.up , ForceMode2D.Impulse);
    }

    public void SetGravitySacleToDefault()
    {
        _rigidbody2D.gravityScale = _defaultGravityScale;
    }

    public void SetGravitySacleToZero()
    {
        _rigidbody2D.gravityScale = 0;
    }

#endregion

#region Private Methods

    private void Awake()
    {
        _rigidbody2D         = GetComponent<Rigidbody2D>();
        _defaultGravityScale = _rigidbody2D.gravityScale;
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position
                                              , 0.15f , groundLayer);
    }


    private void CheckOnLadder()
    {
        _isOnLadder = Physics2D.OverlapCircle(LadderCheck.position
                                              , 0.15f , ladderLayer);
    }

    private void FixedUpdate()
    {
        HandleMovement();
        CheckGround();
        CheckOnLadder();
    }

    private void HandleMovement()
    {
        if (movement.x != 0) SetVelocityX(movement.x);
        else SetVelocityX(0);
        if (IsOnLadder())
        {
            if (movement.y != 0) SetVelocityY(movement.y);
            else SetVelocityY(0);
        }

        movement = Vector2.zero;
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