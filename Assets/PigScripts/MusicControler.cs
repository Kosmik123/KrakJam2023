using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControler : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] musicControlers;


    private void Start()
    {
        audioSource.clip = musicControlers[0];
        audioSource.Play();
    }
    private void Update()
    {
        SwitchMusic();
    }

    public void SwitchMusic()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.clip = musicControlers[0];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource.clip = musicControlers[1];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioSource.clip = musicControlers[2];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioSource.clip = musicControlers[3];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioSource.clip = musicControlers[4];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            audioSource.clip = musicControlers[5];
            audioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            audioSource.clip = musicControlers[6];
            audioSource.Play();
        }
    }
}

