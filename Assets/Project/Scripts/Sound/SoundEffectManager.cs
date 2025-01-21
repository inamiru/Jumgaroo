using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; } // Singleton instance

    public AudioSource audioSource; // AudioSource to play sounds
    public AudioClip arrowKeySE;     // Arrow key sound effect
    public AudioClip returnKeySE; // Return key sound effect
    public AudioClip openPanelSE; // Panel opening sound effect
    public AudioClip playerJumpSE; // Player jump sound effect
    public AudioClip mushroomJumpSE;
    public AudioClip playerDamageSE;

    public AudioClip itemgetSE;
    public AudioClip stargetSE;

    private void Awake()
    {
        // Ensure only one instance of the SoundEffectManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            return;
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not assigned
        }
    }


    // Method to play sound effects
    public void PlaySoundEffect(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip, volume); // Play the sound effect with specified volume
        }
    }

    // Method to play arrow key sound effect specifically
    public void PlayArrowKeySound()
    {
        PlaySoundEffect(arrowKeySE);
    }

    public void PlayReturnKeySound()
    {
        PlaySoundEffect(returnKeySE);
    }

    public void PlayOpenPanelSound()
    {
        PlaySoundEffect(openPanelSE);
    }

    public void PlayPlayerJumpSound()
    {
        PlaySoundEffect(playerJumpSE);
    }
    public void PlayMushroomJumpSound()
    {
        PlaySoundEffect(mushroomJumpSE);
    }
    public void PlayDamageSound()
    {
        PlaySoundEffect(playerDamageSE);
    }

    public void PlayItemGetSound()
    {
        PlaySoundEffect(itemgetSE);
    }

    public void StarGetSound()
    {
        PlaySoundEffect(stargetSE);
    }
}
