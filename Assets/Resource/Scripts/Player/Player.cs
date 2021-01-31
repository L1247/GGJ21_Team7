using System;
using System.Collections;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Player : MonoBehaviour
{
    [Header("行動參數")]
    [SerializeField] private int whichSceneToLoad;
    [SerializeField] private int winSceneIndex;
    public float jumpForce;
    public float runSpeed;
    [SerializeField] private float hitBackForce;
    [SerializeField] private int hitCount;

    [Header("掛載物件")] [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource audioManager;
    [SerializeField] private Transform nextLevelPosition;
    [SerializeField] private SpriteRenderer background;
    public FlashLight flashLight;
    public GameObject logoIcon;

    [Header("獲得能力UI")] [SerializeField] private GameObject upKey;
    [SerializeField] private GameObject downKey;
    [SerializeField] private GameObject rightKey;
    [SerializeField] private GameObject leftKey;
    [SerializeField] private GameObject spaceKey;

    [Header("獲得道具UI")] [SerializeField] private GameObject audioIcon;
    [SerializeField] private GameObject restartIcon;
    [SerializeField] private GameObject flashLightIcon;

    [Header("血條UI")] [SerializeField] private Image[] healthStars;


    [Header("圖片")] [SerializeField] private Sprite[] playerSprite;
    [SerializeField] private Sprite[] backgroundSprite;
    [SerializeField] private Sprite[] upSprite;
    [SerializeField] private Sprite[] downSprite;
    [SerializeField] private Sprite[] rightSprite;
    [SerializeField] private Sprite[] leftSprite;
    [SerializeField] private Sprite[] spaceSprite;
    [SerializeField] private Sprite[] audioSprite;
    [SerializeField] private Sprite[] restartSprite;
    [SerializeField] private Sprite[] flashLightSprite;
    [SerializeField] private Sprite[] healthStar;


    [Header("音效")] [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private AudioClip powerSFX;
    [SerializeField] private AudioClip deadSFX;

    [Header("判斷腳色狀態")] public bool isJump;
    public bool isClimbing;
    public bool getHit;


    [Header("獲得能力")] [SerializeField] bool getLeftKey;

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

    [SerializeField] private bool getLight;



    public bool GetLight
    {
        get => getLight;
        set => getLight = value;
    }
    public bool isGetLogo;


    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        sfxSource = GetComponent<AudioSource>();
        // if (SceneManager.GetActiveScene().buildIndex == 1)
        // {
        //     NoPowerPlayer();
        // }
        // else
        // {
        //     FullPowerPlayer();
        // }
    }


    // Update is called once per frame
    void Update()
    {
        if (!GetBackgroundColor)
        {
            rightKey.GetComponent<Image>().sprite = rightSprite[0];
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                background.sprite = backgroundSprite[0];
            }
            else
            {
                background.sprite = backgroundSprite[2];
            }
        }
        else
        {
            rightKey.GetComponent<Image>().sprite = rightSprite[1];
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                // background.sprite = backgroundSprite[1];
            }
            else
            {
                background.sprite = backgroundSprite[2];
            }
        }

        if (getLeftKey)
        {
            leftKey.SetActive(true);
            leftKey.GetComponent<Image>().sprite = !GetBackgroundColor ? leftSprite[0] : leftSprite[1];
        }
        else
        {
            leftKey.SetActive(false);
        }


        if (Jump)
        {
            spaceKey.SetActive(true);
            spaceKey.GetComponent<Image>().sprite = !GetBackgroundColor ? spaceSprite[0] : spaceSprite[1];
        }
        else
        {
            spaceKey.SetActive(false);
        }

        if (Climb)
        {
            upKey.SetActive(true);
            downKey.SetActive(true);
            if (!GetBackgroundColor)
            {
                upKey.GetComponent<Image>().sprite = upSprite[0];
                downKey.GetComponent<Image>().sprite = downSprite[0];
            }
            else
            {
                upKey.GetComponent<Image>().sprite = upSprite[1];
                downKey.GetComponent<Image>().sprite = downSprite[1];
            }
        }
        else
        {
            upKey.SetActive(false);
            downKey.SetActive(false);
        }

        if (isClimbing)
        {
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody2D.gravityScale = 3;
        }

        if (GetAudio)
        {
            audioIcon.SetActive(true);
            sfxSource.enabled = true;
            audioManager.enabled = true;
            audioIcon.GetComponent<Image>().sprite = !GetBackgroundColor ? audioSprite[0] : audioSprite[1];
        }
        else
        {
            audioIcon.SetActive(false);
            sfxSource.enabled = false;
            audioManager.enabled = false;
        }

        if (GetRestart)
        {
            restartIcon.SetActive(true);
            restartIcon.GetComponent<Image>().sprite = !GetBackgroundColor ? restartSprite[0] : restartSprite[1];
        }
        else
        {
            restartIcon.SetActive(false);
        }

        if (GetLight)
        {
            flashLightIcon.SetActive(true);
            flashLightIcon.GetComponent<Image>().sprite =
                !GetBackgroundColor ? flashLightSprite[0] : flashLightSprite[1];
            if (!isClimbing)
            {
                flashLight.gameObject.SetActive(true);
            }
            else
            {
                flashLight.isTurnOff = true;
                flashLight.gameObject.SetActive(false);
            }
        }
        else
        {
            flashLightIcon.SetActive(false);
            flashLight.gameObject.SetActive(false);
        }

        logoIcon.SetActive(isGetLogo);
    }


    private void FullPowerPlayer()
    {
        sfxSource.enabled = true;
        audioManager.enabled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite[1];
        background.sprite = backgroundSprite[1];
        LeftKey = true;
        GetAnimator = true;
        Jump = true;
        Climb = true;
        GetBackgroundColor = true;
        GetAudio = true;
        GetRestart = true;
        GetLight = false;
        // rightKey.SetActive(true);
        // upKey.SetActive(true);
        // downKey.SetActive(true);
        // leftKey.SetActive(true);
        // spaceKey.SetActive(true);
        // audioIcon.SetActive(true);
        // restartIcon.SetActive(true);
        // flashLightIcon.SetActive(true);
    }

    private void NoPowerPlayer()
    {
        sfxSource.enabled = false;
        audioManager.enabled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite[0];
        background.sprite = backgroundSprite[0];
        rightKey.SetActive(true);
        LeftKey = false;
        Jump = false;
        Climb = false;
        GetRestart = false;
        GetBackgroundColor = false;
        GetAudio = false;
        GetLight = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stair" && Climb && !isClimbing)
        {
            print("Climbing");
            // transform.position = other.transform.position;
            isClimbing = true;
            if (GetLight)
            {
                flashLight.isTurnOff = true;
            }
        }

        if (other.tag == "Enemy")
        {
            GetHit(other);
        }

        if (other.tag == "Item")
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
                    audioManager.volume = 0.5f;
                    audioManager.Play();
                    Destroy(other.gameObject);
                    break;
                case ItemType.BackgroundColor:
                    GetBackgroundColor = true;
                    GetComponent<Animator>().SetTrigger("Idle_C");
                    print("GetColor");
                    var backgroundFade                               = FindObjectOfType<BackgroundFade>();
                    if (backgroundFade) backgroundFade.StartDissolve = true;
                    // transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = playerSprite[1];
                    Destroy(other.gameObject);
                    break;
                case ItemType.Light:
                    print("GetFlashLight");
                    GetLight = true;
                    Destroy(other.gameObject);
                    break;
                case ItemType.Restart:
                    print("Get Restart");
                    GetRestart = true;
                    Destroy(other.gameObject);
                    break;
                case ItemType.Teleport:
                    print("Teleport to : " + whichSceneToLoad+1);
                    StartCoroutine(TeleportToScene(whichSceneToLoad+1));
                    other.GetComponent<BoxCollider2D>().isTrigger = false;
                    break;
                case ItemType.Icon:
                    isGetLogo = true;
                    Destroy(other.gameObject);
                    break;
                case ItemType.Level2Teleport:
                    if (isGetLogo)
                    {
                        StartCoroutine(TeleportToScene(whichSceneToLoad+1));
                        other.GetComponent<BoxCollider2D>().isTrigger = false;
                    }

                    break;
                default:
                    break;
            }

            if (GetAudio)
            {
                PlayPowerSFX();
            }
        }

        if (isGetLogo && other.name == " ")
        {
            StartCoroutine(WinGame());
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Stair" && isClimbing)
        {
            isClimbing = false;
            print("StopClimbing");
            _rigidbody2D.velocity = other.transform.position.y > transform.position.y
                ? new Vector2(0, -jumpForce)
                : new Vector2(0, jumpForce);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag=="Floor")
        {
            isJump = false;
        }
    }

    public void PlayJumpSFX()
    {
        sfxSource.PlayOneShot(jumpSFX);
    }

    public void GetHit(Collider2D other)
    {
        getHit = true;
        isJump = false;
        isClimbing = false;
        if (GetAnimator)
        {
            GetComponent<Animator>().SetTrigger(!GetBackgroundColor ? "Hurt" : "Hurt_C");
        }

        if (other.transform.position.x > transform.position.x)
        {
            _rigidbody2D.velocity = new Vector2(-hitBackForce, 0);
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(hitBackForce, 0);
        }

        hitCount--;
        healthStars[hitCount].sprite = healthStar[1];
        StartCoroutine(HitBack());
        print("Current hit is : " + hitCount);
        if (GetAudio)
        {
            sfxSource.PlayOneShot(hitSFX);
        }

        var isDead = hitCount <= 0;
        if (isDead == false)
            Camera.main?.transform.DOShakePosition(0.5f);
        if (isDead)
        {
            print("Dead");
            if (GetAudio)
            {
                PlayDeadSFX();
            }

            if (GetRestart)
            {
                StartCoroutine(BackTo(whichSceneToLoad));
            }
            else
            {
                transform.GetChild(1).SetParent(background.transform);
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

    IEnumerator TeleportToScene(int index)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(index);
    }

    IEnumerator BackTo(int index)
    {
        index = whichSceneToLoad;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }

    IEnumerator HitBack()
    {
        yield return new WaitForSeconds(1f);
        getHit = false;
    }

    IEnumerator WinGame()
    {
        print("Win");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(winSceneIndex);
    }
}