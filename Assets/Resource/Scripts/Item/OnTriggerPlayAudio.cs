using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPlayAudio  : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;

    // Start is called before the first frame update
    private void OnDestroy()
    {
        var audioSource = FindObjectOfType<AudioSource>();
        audioSource?.PlayOneShot(_audioClip);
    }
}