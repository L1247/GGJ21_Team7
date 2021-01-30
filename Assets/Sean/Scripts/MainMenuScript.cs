using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;


    float fSpeed=10;
    float fTime;
    /// <summary>
    /// 動畫結束
    /// </summary>
    bool isAniEnd;
    /// <summary>
    /// 音樂結束
    /// </summary>
    bool isAudioEnd;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"遊戲開始，Speed:{fSpeed}");
        StartCoroutine(coAnimator());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        AudioClose();
    }

    void PlayerMove()
    {
        if (isAniEnd && Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.Translate(Vector3.right * fSpeed * Time.deltaTime);
        }
    }
    void AudioClose()
    {
        if (isAudioEnd)
        {
            fTime += Time.deltaTime;
            audio.volume = 1- fTime;
            if (audio.volume==0)
            {
                audio.volume = 0;
            }
        }
    }

    IEnumerator coAnimator()
    {
        yield return new WaitForSeconds(3.2f);
        isAudioEnd = true;
        yield return new WaitForSeconds(2);
        isAniEnd = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name== "GameStartSite")
        {
            Debug.Log($"進入遊戲場景");
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
