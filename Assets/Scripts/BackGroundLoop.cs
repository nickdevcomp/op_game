using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    public AudioClip[] backgroundSounds; // Массив звуков, которые будут проигрываться на заднем фоне
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayBackgroundSounds());
    }

    private IEnumerator PlayBackgroundSounds()
    {
        while (true)
        {
            foreach (var sound in backgroundSounds)
            {
                audioSource.clip = sound;
                audioSource.Play();
                yield return new WaitForSeconds(sound.length); 
            }
        }
    }
}