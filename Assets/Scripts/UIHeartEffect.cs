using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UIHeartEffect : MonoBehaviour
{
    [SerializeField]
    private List<Image> _heartImages = new List<Image>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (var heartImage in _heartImages)
        {
            heartImage.sprite.ObserveEveryValueChanged(s => s)
                      .Subscribe(sprite => OnSpriteChanged(heartImage , sprite));
        }
    }

    private void OnSpriteChanged(Image heartImage , Sprite sprite)
    {
        Debug.Log($"{heartImage} , {sprite}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
