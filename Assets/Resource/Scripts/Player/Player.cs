using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("行動參數")]
    // [SerializeField] float timer = 0;
    // [SerializeField] private float jumpColdTime;
    public float jumpForce;
    public float runSpeed;
    [SerializeField] private float hitBackForce;
    [SerializeField] private int hitCount;
    public int PlayerHitCount
    {
        get => hitCount;
    }

    [Header("掛載物件")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource audioManager;
    [SerializeField] private Transform nextLevelPosition;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private GameObject upKey;
    [SerializeField] private GameObject downKey;
    [SerializeField] private GameObject rightKey;
    [SerializeField] private GameObject leftKey;
    [SerializeField] private GameObject spaceKey;


    [Header("圖片")]
    [SerializeField] private Sprite[] playerSprite;
    [SerializeField] private Sprite[] backgroundSprite;
    [SerializeField] private Sprite[] upSprite;
    [SerializeField] private Sprite[] downSprite;
    [SerializeField] private Sprite[] rightSprite;
    [SerializeField] private Sprite[] leftSprite;
    [SerializeField] private Sprite[] spaceSprite;



    [Header("音效")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip powerSFX;
    [SerializeField] private AudioClip deadSFX;

    [Header("判斷腳色狀態")]
    public bool isJump;
    public bool isClimbing;
    public bool getHit;

    [Header("獲得能力")]
    [SerializeField] bool getLeftKey;
    public bool LeftKey
    {
        get => getLeftKey;
        set => getLeftKey = value;
    }

    [SerializeField] private bool getJump;
    public bool Jump
    {
        get => getJump;
        set => getJump = value;
    }

    [SerializeField] private bool getClimb;
    public bool Climb
    {
        get => getClimb;
        set => getClimb = value;
    }

    [SerializeField] private bool getAnimator;
    public bool GetAnimator
    {
        get => getAnimator;
        set => getAnimator = value;
    }

    [SerializeField] private bool getAudio;
    public bool GetAudio
    {
        get => getAudio;
        set => getAudio = value;
    }

    [SerializeField] private bool getBackgroundColor;
    public bool GetBackgroundColor
    {
        get => getBackgroundColor;
        set => getBackgroundColor = value;
    }

    [SerializeField] private bool getRestart;
    public bool GetRestart
    {
        get => getRestart;
        set => getRestart = value;
    }

    private bool getLight;


    public bool GetLight
    {
        get => getLight;
        set => getLight = value;
    }



    void Start()
    {
        // getLeftKey = false;
        // getJump = false;
        // getAnimator = false;
        // getAudio = false;
        // getBackgroundColor = false;
        // getLight = false;
        // getClimb = false;
        //
        // timer = 0;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        sfxSource = GetComponent<AudioSource>();
        sfxSource.enabled = false;
        audioManager.enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite[0];
        background.sprite = backgroundSprite[0];
        rightKey.SetActive(true);
        upKey.SetActive(false);
        downKey.SetActive(false);
        leftKey.SetActive(false);
        spaceKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetBackgroundColor)
        {
            rightKey.GetComponent<Image>().sprite = rightSprite[0];
        }
        else
        {
            rightKey.GetComponent<Image>().sprite = rightSprite[1];
        }
        if (getLeftKey && !GetBackgroundColor)
        {
            leftKey.SetActive(true);
            leftKey.GetComponent<Image>().sprite = leftSprite[0];
        }
        else if(getLeftKey && GetBackgroundColor)
        {
            leftKey.SetActive(true);
            leftKey.GetComponent<Image>().sprite = leftSprite[1];
        }
        else
        {
            leftKey.SetActive(false);
        }

        if (Climb && !GetBackgroundColor)
        {
            upKey.SetActive(true);
            upKey.GetComponent<Image>().sprite = upSprite[0];
            downKey.SetActive(true);
            downKey.GetComponent<Image>().sprite = downSprite[0];

        }
        else if (Climb && GetBackgroundColor)
        {
            upKey.SetActive(true);
            upKey.GetComponent<Image>().sprite = upSprite[1];
            downKey.SetActive(true);
            downKey.GetComponent<Image>().sprite = downSprite[1];
        }
        else
        {
            upKey.SetActive(false);
            downKey.SetActive(false);
        }

        if (Jump && !GetBackgroundColor)
        {
            spaceKey.SetActive(true);
            spaceKey.GetComponent<Image>().sprite = spaceSprite[0];
        }
        else if (Jump && GetBackgroundColor)
        {
            spaceKey.SetActive(true);
            spaceKey.GetComponent<Image>().sprite = spaceSprite[1];
        }
        else
        {
            spaceKey.SetActive(false);
        }

        if (isClimbing)
        {
            _rigidbody2D.gravityScale=0;
            _rigidbody2D.velocity = new Vector2(0, 0);
        }
        else
        {
            _rigidbody2D.gravityScale=3;
        }

        // if (!GetBackgroundColor)
        // {
        //     transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerLine;
        //     background.sprite = backgroundLine;
        // }
        // else
        // {
        //     transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerFullColor;
        //     background.sprite = backgroundFullColor;
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Stair")
        {
            if (Climb && !isClimbing)
            {
                print("Climbing");
                // transform.position = other.transform.position;
                isClimbing = true;
                // _rigidbody2D.velocity = new Vector2(0, jumpForce / 2);
            }
        }
        if (other.tag=="Enemy")
        {
            GetHit(other);
        }

        if (other.tag=="Item")
        {
            ItemType itemType = other.GetComponent<Item>().itemType;
            switch (itemType)
            {
                case ItemType.LeftKey:
                    LeftKey = true;
                    print("GetLeftKey");
                    Destroy(other.gameObject);
                    break;
                case ItemType.Jump:
                    Jump = true;
                    print("GetJump");
                    Destroy(other.gameObject);
                    break;
                case ItemType.Climb:
                    Climb = true;
                    print("GetClimb");
                    Destroy(other.gameObject);
                    break;
                case ItemType.Animation:
                    GetAnimator = true;
                    print("GetAnimation");
                    Destroy(other.gameObject);
                    break;
                case ItemType.Audio:
                    GetAudio = true;
                    print("GetSound");
                    sfxSource.enabled = true;
                    audioManager.enabled = true;
                    audioManager.volume = 0.5f;
                    audioManager.Play();
                    Destroy(other.gameObject);
                    break;
                case ItemType.BackgroundColor:
                    GetBackgroundColor = true;
                    print("GetColor");
                    transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite[1];
                    background.sprite = backgroundSprite[1];
                    Destroy(other.gameObject);
                    break;
                case ItemType.Light:
                    print("Light");
                    GetLight = true;
                    Destroy(other.gameObject);
                    break;
                case ItemType.Restart:
                    print("Get Restart");
                    GetRestart = true;
                    Destroy(other.gameObject);
                    break;
                case ItemType.Teleport :
                    print("Teleport to : "+ nextLevelPosition.position);
                    TeleportToNextLevel();
                    break;
                default:
                    break;
            }
            if (GetAudio)
            {
                PlayPowerSFX();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag=="Stair" && isClimbing)
        {
            isClimbing = false;
            print("StopClimbing");
            if (other.transform.position.y>transform.position.y)
            {
                _rigidbody2D.velocity = new Vector2(0, -jumpForce);
            }
            else
            {
                _rigidbody2D.velocity = new Vector2(0, jumpForce);
            }


        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag=="Floor")
        {
            isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag=="Floor")
        {
           isJump=false;
        }
    }

    public void PlayJumpSFX()
    {
        sfxSource.PlayOneShot(jumpSFX);
    }

    public void GetHit(Collider2D other)
    {
        getHit = true;
        if (other.transform.position.x> transform.position.x)
        {
            _rigidbody2D.velocity = new Vector2(-hitBackForce, 0);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(hitBackForce, 0);
        }
        hitCount--;
        StartCoroutine(HitBack());
        print("Current hit is : "+ hitCount);
        if (GetAudio)
        {
            sfxSource.PlayOneShot(hitSFX);
        }

        if (hitCount<=0)
        {
            print("Dead");
            if (GetAudio)
            {
                PlayDeadSFX();
            }

            if (GetRestart)
            {
                StartCoroutine(BackToTitle());
            }
            else
            {
                gameObject.SetActive(false);
            }

        }
    }

    private void PlayDeadSFX()
    {
        sfxSource.PlayOneShot(deadSFX);
    }

    public void PlayPowerSFX()
    {
        sfxSource.PlayOneShot(powerSFX);
    }

    public void TeleportToNextLevel()
    {
        transform.position = nextLevelPosition.position;
    }

    IEnumerator BackToTitle()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }

    IEnumerator HitBack()
    {
        yield return new WaitForSeconds(1f);
        getHit = false;
    }

}