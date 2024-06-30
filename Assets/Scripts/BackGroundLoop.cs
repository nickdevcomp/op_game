using System.Collections;
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
        while (true) // review(30.06.2024): Не хватает способа выключить эти звуки, бесконечный цикл - не лучшая идея, стоило добавить хотя бы флажок какой-нибудь
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