using UnityEngine;

public class AIController : MonoBehaviour
{
    #region 宣告
    [Header("移動速度")]
    [SerializeField] float moveSpeed = 2f;
    [Header("偵測距離")]
    [SerializeField] float distance = 5f;

    [Header("巡邏範圍")]
    [SerializeField] Transform rightWall;
    [SerializeField] Transform leftWall;
    [Header("停留時間")]
    [SerializeField] float startStayTime = .1f;

    private GameObject player;
    private Animator animator;
    private SpriteRenderer sprite;

    private bool IsMoveRight;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (IsInRange())
        {
            FollowBehaviour();
        }
        else
        {
            PatrolBehaviour();
        }
    }

    private void FollowBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

        if (IsPlayerOnRight())
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }

    private void PatrolBehaviour()
    {
        if (IsMoveRight)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            sprite.flipX = false;
        }
        else
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            sprite.flipX = true;
        }
    }

    private bool IsInRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) < distance;
    }

    private bool IsPlayerOnRight()
    {
        return player.transform.position.x - transform.position.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            IsMoveRight = !IsMoveRight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}
