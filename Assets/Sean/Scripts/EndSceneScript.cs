using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneScript : MonoBehaviour
{
    [SerializeField]
    Animator endAnimator;
    [SerializeField]
    GameObject objLogo;
    [SerializeField]
    Button btnStart;
    [SerializeField]
    Button btnEnd;

    Vector2 v2Scale;
    Vector2 v2AddForce;
    float fSpeed=10;
    bool isGameEnd;
    bool isOnFloor;
    bool isClickRe;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"結束場景，Speed:{fSpeed}");
        v2Scale = transform.localScale;
        v2AddForce.y = 10;
        btnStart.onClick.AddListener(() => ReStart());
        btnEnd.onClick.AddListener(()=> GameEnd());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        AnimatorRun();
        ReGame();
    }
    void AnimatorRun()
    {
        if (isGameEnd && isOnFloor)
        {
            objLogo.SetActive(false);
            endAnimator.SetBool("TheEnd", true);
            Debug.Log($"觸發結束動畫");
        }
    }
    void PlayerMove()
    {
        if (!isGameEnd)
        {
            v2Scale.y = 1;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Translate(Vector2.left * fSpeed * Time.deltaTime);
                v2Scale.x = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Translate(Vector2.right * fSpeed * Time.deltaTime);
                v2Scale.x = 1;
            }
            transform.localScale = v2Scale;
            if (Input.GetKeyDown(KeyCode.Space) && isOnFloor)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero; //跳躍前清空速度
                GetComponent<Rigidbody2D>().AddForce(v2AddForce, ForceMode2D.Impulse);
            }
        }
    }
    void ReStart()
    {
        isClickRe = true;
    }
    void GameEnd()
    {
        Debug.Log($"遊戲結束");
        Application.Quit();
    }
    void ReGame()
    {
        if (isClickRe)
        {
            endAnimator.SetTrigger("ReGame");
            if (endAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReAnimation"))
            {
                Debug.Log($"播放LOGO結束動畫");
            }
            else
            {
                StartCoroutine(coReMainScene());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "GameEndSite")
        {
            isGameEnd = true;
            Debug.Log($"遊戲結束{isGameEnd}");
        }
        if (collision.transform.name== "Floor")
        {
            isOnFloor = true;
            Debug.Log($"碰觸地面{isOnFloor}");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name== "Floor")
        {
            Debug.Log($"離開地面");
            isOnFloor = false;
        }
    }
    IEnumerator coReMainScene()
    {
        yield return new WaitForSeconds(2.5f);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainMenuScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
