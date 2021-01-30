using UnityEngine;
public class Frank_PlayerController : MonoBehaviour
{
    [Tooltip("移動速度")]
    [SerializeField] float moveSpeed = 5f;
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
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("碰到怪物");
        }
    }
}
