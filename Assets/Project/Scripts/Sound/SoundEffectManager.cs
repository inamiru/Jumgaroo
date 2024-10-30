using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance; // Singleton instance

    public AudioSource audioSource; // AudioSource to play sounds
    public AudioClip arrowKeySE;     // Arrow key sound effect
    public AudioClip returnKeySE; // リターンキーのサウンドエフェクト

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
        }

        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource if not assigned
    }

    // Method to play sound effects
    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); // Play the sound effect
        }
        else
        {
            Debug.LogWarning("Attempted to play a null sound clip.");
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
}
