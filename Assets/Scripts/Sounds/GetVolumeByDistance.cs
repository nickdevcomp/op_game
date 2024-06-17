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

    public bool isSpeech;

    private void Start() => audioSource = GetComponent<AudioSource>();

    void Update()
    {
        var distance = Vector2.Distance(player.position, transform.position);

        if (isSpeech)
            HandleSpeech(distance);
        else
            HandleBackgroundSound(distance);

        AdjustVolume(distance);
    }

    private void HandleSpeech(float distance)
    {
        if (PlayerController.IsAudioPlaying || audioSource.isPlaying)
            return;

        if (!(distance <= DistanceToActivate) || count >= PlayingCount) 
            return;
        
        audioSource.Play();
        PlayerController.IsAudioPlaying = true;
        Invoke(nameof(ResetAudioPlayingFlag), audioSource.clip.length);
        count += 1;
    }

    private void HandleBackgroundSound(float distance)
    {
        if (audioSource.isPlaying)
            return;

        if (!(distance <= DistanceToActivate) || count >= PlayingCount) 
            return;
        
        audioSource.Play();
        count += 1;
    }

    private void AdjustVolume(float distance)
    {
        var volume = maxVolume - distance / volumeRange * (maxVolume - minVolume);
        audioSource.volume = Mathf.Clamp(volume, minVolume, maxVolume);
    }

    private void ResetAudioPlayingFlag() => PlayerController.IsAudioPlaying = false;
}