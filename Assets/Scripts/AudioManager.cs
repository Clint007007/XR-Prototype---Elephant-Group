using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioSource backgroundMusic;

    [Header("Elephant Sounds")]
    public AudioSource footstepsSource;
    public AudioSource elephantVoiceSource;
    public AudioClip elephantRoar;
    public AudioClip deathSound;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayFootsteps()
    {
        if (!footstepsSource.isPlaying)
            footstepsSource.Play();
    }

    public void StopFootsteps()
    {
        footstepsSource.Stop();
    }

    public void PlayAttackSounds()
    {
        footstepsSource.Stop();
        elephantVoiceSource.clip = elephantRoar;
        elephantVoiceSource.Play();
        // Play death sound after the roar finishes
        Invoke(nameof(PlayDeathSound), elephantRoar.length);
    }

    void PlayDeathSound()
    {
        elephantVoiceSource.clip = deathSound;
        elephantVoiceSource.Play();
    }

    public void PlayWalkAwaySounds()
    {
        // Footsteps play while walking away then stop
        PlayFootsteps();
    }

    public void StopAll()
    {
        footstepsSource.Stop();
        elephantVoiceSource.Stop();
    }
}