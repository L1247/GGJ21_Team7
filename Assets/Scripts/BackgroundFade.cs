using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundFade : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    private float threshold = 0.33f;

    public bool StartDissolve;

    // Update is called once per frame
    void Update()
    {
        if (StartDissolve && threshold<=1)
        {
            threshold += 0.0005f;
            _renderer.material.SetFloat("_Threshold" , threshold);
        }
    }
}