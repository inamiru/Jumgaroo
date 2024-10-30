using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMSoundManager : MonoBehaviour
{
     public static BGMSoundManager Instance;

    public AudioSource bgmSource;
    public AudioClip titleBGM;   // タイトル画面用BGM
    public AudioClip stageSelectBGM;   // ステージ選択画面用BGM
    public AudioClip stage01BGM;   // ステージ01用BGM
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // シーンをまたいでBGMを再生する
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM再生メソッド
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip) return; // 既に同じBGMが再生されている場合は何もしない
        
        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    
     // タイトル画面のBGMを再生
    public void PlayTitleBGM()
    {
        PlayBGM(titleBGM);
    }

    // ステージ選択画面のBGMを再生
    public void PlayStageSelectBGM()
    {
        PlayBGM(stageSelectBGM);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position); // サウンドをカメラ位置で再生
        }
    }
}
