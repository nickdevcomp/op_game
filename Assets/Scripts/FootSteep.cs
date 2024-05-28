using UnityEngine;

public class PlayFootstepSound : MonoBehaviour
{
    public AudioSource footstepSound;

    public void PlayFootstep()
    {
        footstepSound.Play();
    }
}