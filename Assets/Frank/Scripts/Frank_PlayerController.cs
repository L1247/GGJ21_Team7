using UnityEngine;
public class Frank_PlayerController : MonoBehaviour
{
    [Tooltip("移動速度")]
    [SerializeField] float moveSpeed = .1f;
    [Tooltip("跳躍力道")]
    [SerializeField] float jumpForce = 6f;

    Rigidbody2D rig2D;

    private void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
