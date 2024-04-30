using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        FrogMovement.OnFart += OnFart;
    }

    private void OnFart()
    {
        int index = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[index]);
    }
   
}
