using UnityEngine;

public class AIController : MonoBehaviour
{
    #region 宣告
    [Header("初始值")]
    [SerializeField] float positiveLocalScale;
    [SerializeField] float negativeLocalScale;

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

    private bool IsMoveRight;
    private float time;
    #endregion

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (time >= stayTime)
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
            transform.localScale = new Vector2(negativeLocalScale, positiveLocalScale);
            warmIcon.flipX = true;
        }

        animator.SetBool("IsWalk", true);

        warmIcon.sprite = exclamationIcon;
    }

    private void PatrolBehaviour()
    {
        if (IsMoveRight)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(positiveLocalScale, positiveLocalScale);
            warmIcon.flipX = false;
        }
        else
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.localScale = new Vector2(negativeLocalScale, positiveLocalScale);
            warmIcon.flipX = true;
        }

        animator.SetBool("IsWalk", true);

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
            IsMoveRight = !IsMoveRight;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
