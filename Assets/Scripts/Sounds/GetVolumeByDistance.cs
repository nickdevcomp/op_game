using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform player;
    public float maxVolume = 1f;
    public float minVolume;
    public float volumeRange = 5f;
    private bool isPlayed;
    public float DistanceToActivate;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);

        if (!audioSource.isPlaying && distance <= DistanceToActivate && !isPlayed)
        {
            audioSource.Play();
            isPlayed = true;
        }

        var volume = maxVolume - distance / volumeRange * (maxVolume - minVolume);
        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}