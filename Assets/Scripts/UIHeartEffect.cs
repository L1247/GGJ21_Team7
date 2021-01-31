using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
            var animateHeart = Instantiate(heartImage.gameObject, heartImage.transform.parent);
            heartImage.ObserveEveryValueChanged(s => s.sprite)
                      .Where(sprite =>sprite.name == "血量_線稿")
                      .Subscribe(sprite => OnSpriteChanged(heartImage,animateHeart));
        }
    }

    private void OnSpriteChanged(Image heartImage , GameObject animateHeart)
    {
        Debug.Log($"{heartImage.sprite}");
        var image = animateHeart.GetComponent<Image>();
        animateHeart.transform.DOMoveY(1.5f , 5).SetEase(Ease.OutQuad);
        animateHeart.transform.DORotate(new Vector3(0,0,-45) , 0.8f).SetEase(Ease.InQuad);
        image.DOFade(0 , 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
