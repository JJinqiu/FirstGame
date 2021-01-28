using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio;
    [SerializeField]
    private AudioClip hurtAudio;
    [SerializeField]
    private AudioClip collectionsAudio;

    private void Awake()
    {
        instance = this;
    }

    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }
    
    public void CollectionsAudio()
    {
        audioSource.clip = collectionsAudio;
        audioSource.Play();
    }
}
