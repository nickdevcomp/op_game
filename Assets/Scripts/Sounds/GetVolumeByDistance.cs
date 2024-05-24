using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform player;
    public float maxVolume = 1f;
    public float minVolume;
    public float volumeRange = 5f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        var distance = Vector2.Distance(player.position, transform.position);
        var volume = maxVolume - distance / volumeRange * (maxVolume - minVolume);
        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}