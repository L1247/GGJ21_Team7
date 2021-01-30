using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject highLight;
    public bool isTurnOff;
    private EdgeCollider2D collider;

    private void Start()
    {
        collider = GetComponent<EdgeCollider2D>();
        isTurnOff=true;

    }

    private void Update()
    {
        if (isTurnOff)
        {
            highLight.SetActive(false);
            collider.enabled = false;
        }
        else
        {
            highLight.SetActive(true);
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name=="暗格")
        {
            other.gameObject.SetActive(false);
        }
    }
}
