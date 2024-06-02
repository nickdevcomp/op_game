using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Transform player;
    public float maxVolume = 1f;
    public float minVolume;
    public float volumeRange = 5f;
    public float DistanceToActivate;
    
    public int PlayingCount;
    private int count;

    private void Start() =>  audioSource = GetComponent<AudioSource>();

    void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);

        if (!audioSource.isPlaying && distance <= DistanceToActivate)
        {
            if (count < PlayingCount)
            {
                audioSource.Play();
                count += 1;
            }
        }

        var volume = maxVolume - distance / volumeRange * (maxVolume - minVolume);
        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }
}