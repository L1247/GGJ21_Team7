using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("行動參數")]
    // [SerializeField] float timer = 0;
    // [SerializeField] private float jumpColdTime;
    public float jumpForce;
    public float runSpeed;

    [Header("掛載物件")]
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource audioManager;
    [SerializeField] private Transform nextLevelPosition;
    [SerializeField] private SpriteRenderer background;

    [Header("圖片")]
    [SerializeField] private Sprite playerLine;
    [SerializeField] private Sprite playerFullColor;
    [SerializeField] private Sprite backgroundLine;
    [SerializeField] private Sprite backgroundFullColor;



    [Header("音效")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip powerSFX;

    [Header("判斷腳色狀態")]
    public bool isJump;
    public bool isClimbing;

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

    [SerializeField] private bool getLight;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (isJump)
        {

        }

        if (isClimbing)
        {
            _rigidbody2D.gravityScale=0;
            _rigidbody2D.velocity = new Vector2(0, 0);
        }
        else
        {
            _rigidbody2D.gravityScale=1;
        }

        if (!GetBackgroundColor)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerLine;
            background.sprite = backgroundLine;
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerFullColor;
            background.sprite = backgroundFullColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Stair")
        {
            if (Climb && !isClimbing)
            {
                print("Climbing");
                transform.position = other.transform.position;
                isClimbing = true;
                // _rigidbody2D.velocity = new Vector2(0, jumpForce / 2);
            }
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
                    print("GetJump");
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
                    Destroy(other.gameObject);
                    break;
                case ItemType.Light:
                    print("Light");
                    GetLight = true;
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
            _rigidbody2D.velocity = new Vector2(0, jumpForce / 2);

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

    public void PlayHitSFX()
    {
        sfxSource.PlayOneShot(hitSFX);
    }

    public void PlayPowerSFX()
    {
        sfxSource.PlayOneShot(powerSFX);
    }

    public void TeleportToNextLevel()
    {
        transform.position = nextLevelPosition.position;
    }

}