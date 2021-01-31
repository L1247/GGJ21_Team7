using UnityEngine;

enum DirectionType
{
    Right,
    Left
}

public class AIController : MonoBehaviour
{
    #region 變數宣告
    [Header("初始值")]
    [SerializeField] float positiveLocalScale;
    [SerializeField] float negativeLocalScale;

    [Header("移動速度")]
    [SerializeField] float moveSpeed = 1.5f;
    [Header("加速倍數")]
    [SerializeField] float speedModifier = 2f;
    [Header("追趕距離")]
    [SerializeField] float chaseDistance = 5f;
    [Header("巡邏範圍")]
    [SerializeField] Transform rightWall;
    [SerializeField] Transform leftWall;

    [Header("放置圖片")]
    [SerializeField] SpriteRenderer warmIcon;
    [SerializeField] Sprite questionIcon;
    [SerializeField] Sprite exclamationIcon;

    private GameObject player;
    private Animator animator;
    private Player playerScript;

    private bool isMoveRight;

    private DirectionType directionType;
    private int randomNumber;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();

        StartDirection();
    }

    private void StartDirection()
    {
        randomNumber = Random.Range(0, 2);

        if (randomNumber == 0)
        {
            directionType = DirectionType.Right;
        }
        else if (randomNumber == 1)
        {
            directionType = DirectionType.Left;
        }

        switch (directionType)
        {
            case DirectionType.Right:
                isMoveRight = true;
                break;
            case DirectionType.Left:
                isMoveRight = false;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (!playerScript.getHit)
        {
            if (IsInChaseRange())
            {
                FollowBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
        }

        WarmBehaviour();
    }

    private void FollowBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * speedModifier * Time.deltaTime);

        if (IsPlayerOnRight())
        {
            transform.localScale = new Vector2(positiveLocalScale, positiveLocalScale);
            warmIcon.flipX = false;
        }
        else
        {
            transform.localScale = new Vector2(-negativeLocalScale, positiveLocalScale);
            warmIcon.flipX = true;
        }

        animator.SetBool("IsChase", true);
        animator.SetBool("IsWalk", false);

        warmIcon.sprite = exclamationIcon;
    }

    private void PatrolBehaviour()
    {
        if (isMoveRight)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(positiveLocalScale, positiveLocalScale);
            warmIcon.flipX = false;
        }
        else
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(-negativeLocalScale, positiveLocalScale);
            warmIcon.flipX = true;
        }

        animator.SetBool("IsWalk", true);
        animator.SetBool("IsChase", false);

        warmIcon.sprite = questionIcon;
    }

    private void WarmBehaviour()
    {
        if (playerScript.getHit)
        {
            animator.SetBool("IsWarm", true);
        }
        else
        {
            animator.SetBool("IsWarm", false);
        }
    }

    private bool IsInChaseRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) < chaseDistance;
    }

    private bool IsPlayerOnRight()
    {
        return player.transform.position.x - transform.position.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            isMoveRight = !isMoveRight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
