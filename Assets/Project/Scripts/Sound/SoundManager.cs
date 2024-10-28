using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;  // シングルトンインスタンス
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject soundManagerObj = new GameObject("SoundManager");
                    _instance = soundManagerObj.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    public AudioSource bgmSource; // BGM再生用のAudioSource
    public AudioClip stageBGM;    // ステージBGMのAudioClip

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    // タイトルのBGMを再生
    public void PlayTitleBGM()
    {
        if (bgmSource != null && stageBGM != null)
        {
            bgmSource.clip = stageBGM;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM AudioSource or AudioClip is missing.");
        }
    }

    // BGMを停止
    public void StopBGM()
    {
        if (bgmSource != null)
        {
            bgmSource.Stop();
        }
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position); // サウンドをカメラ位置で再生
        }
    }
}
