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
    public AudioClip stage02BGM;   // ステージ02用BGM
    public AudioClip stage03BGM;   // ステージ03用BGM
    public AudioClip stage04BGM;   // ステージ04用BGM
    public AudioClip resultBGM;   // リザルト用BGM
    public AudioClip gameclearBGM;   // ステージクリア用BGM
    public AudioClip gameoverBGM;   // ゲームオーバー用BGM

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            if (bgmSource == null)
            {
                bgmSource = gameObject.AddComponent<AudioSource>(); // bgmSourceが未設定なら追加
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM再生メソッド
    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource == null || bgmSource.clip == clip) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.Play();
    }
    
    public void PlayTitleBGM() => PlayBGM(titleBGM);
    public void PlayStageSelectBGM() => PlayBGM(stageSelectBGM);
    public void PlayStage01BGM() => PlayBGM(stage01BGM);
    public void PlayStage02BGM() => PlayBGM(stage02BGM);
    public void PlayStage03BGM() => PlayBGM(stage03BGM);
    public void PlayStage04BGM() => PlayBGM(stage04BGM);
    public void PlayResultBGM() => PlayBGM(resultBGM);
    public void PlayGameClearBGM() => PlayBGM(gameclearBGM);
    public void PlayGameOverBGM() => PlayBGM(gameoverBGM);

    // BGMを停止
    public void StopBGM()
    {
        if (bgmSource != null)
            bgmSource.Stop();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position); // サウンドをカメラ位置で再生
        }
    }
}
