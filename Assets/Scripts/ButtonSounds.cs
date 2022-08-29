using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSounds : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip hoverSFX;
    [SerializeField] AudioClip selectSFX;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayHoverSFX()
    {
        audioSource.PlayOneShot(hoverSFX);
    }
    public void PlaySelectSFX()
    {
        audioSource.PlayOneShot(selectSFX);
    }
}
