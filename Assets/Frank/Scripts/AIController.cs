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

    [Header("抓取物件")]
    [SerializeField] Player playerScript;

    [Header("移動速度")]
    [SerializeField] float moveSpeed = 1.5f;
    [Header("加速倍數")]
    [SerializeField] float speedModifier = 2f;
    [Header("攻擊距離")]
    [SerializeField] float attackDistance = 1.5f;
    [Header("追趕距離")]
    [SerializeField] float chaseDistance = 5f;
    [Header("巡邏範圍")]
    [SerializeField] Transform rightWall;
    [SerializeField] Transform leftWall;
    [Header("停滯時間")]
    [SerializeField] float stayTime = 3;

    [Header("放置圖片")]
    [SerializeField] SpriteRenderer warmIcon;
    [SerializeField] Sprite questionIcon;
    [SerializeField] Sprite exclamationIcon;

    private GameObject player;
    private Animator animator;

    private bool isMoveRight;
    private float time;

    private DirectionType directionType;
    private int randomNumber;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();


        randomNumber = Random.Range(0, 2);

        if (randomNumber == 0)
        {
            directionType = DirectionType.Right;
        }
        else if (randomNumber == 1)
        {
            directionType = DirectionType.Left;
        }
    }

    private void Update()
    {
        if (time >= stayTime && !playerScript.getHit)
        {
            if (IsInChaseRange())
            {
                if (IsInAttackRange())
                {
                    AttackBehaviour();
                }
                else
                {
                    FollowBehaviour();
                }
            }
            else
            {
                PatrolBehaviour();
            }
        }

        UpdateTimer();
    }

    private void UpdateTimer()
    {
        time += Time.deltaTime;
    }

    private void AttackBehaviour()
    {
        time = 0;
        animator.SetBool("IsWalk", false);
    }

    private void FollowBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), moveSpeed * speedModifier * Time.deltaTime);

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
        switch (directionType)
        {
            case DirectionType.Right:
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector2(positiveLocalScale, positiveLocalScale);
                warmIcon.flipX = false;
                break;
            case DirectionType.Left:
                transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                transform.localScale = new Vector2(-negativeLocalScale, positiveLocalScale);
                warmIcon.flipX = true;
                break;
            default:
                break;
        }

        animator.SetBool("IsWalk", true);
        animator.SetBool("IsChase", false);

        warmIcon.sprite = questionIcon;
    }

    private bool IsInChaseRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) < chaseDistance;
    }

    private bool IsInAttackRange()
    {
        return Vector2.Distance(transform.position, player.transform.position) < attackDistance;
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
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
