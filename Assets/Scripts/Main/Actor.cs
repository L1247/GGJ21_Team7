using UnityEngine;

public class Actor : MonoBehaviour
{
#region Private Variables

    private Rigidbody2D _rigidbody2D;

#endregion

#region Public Methods

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

#endregion
}