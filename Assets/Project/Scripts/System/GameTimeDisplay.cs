using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimeDisplay : MonoBehaviour
{
    public TextMeshProUGUI timeText;   // 経過時間を表示するTextMeshProのUI要素
    
    public static GameTimeDisplay Instance { get; private set; }

    private float startTime;
    private float finishTime;
    private bool isFinished;
    private bool isStarted;  // タイマーが開始されたかどうか
    private bool isCountdownActive;  // カウントダウン中かどうか


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 他のシーンに移行しても削除されないようにする
        }
        else if (Instance != this)
        {
            Destroy(gameObject);  // 重複するインスタンスを削除
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isFinished = false;
        isStarted = false;  // タイマーを未開始に設定
        isCountdownActive = true;  // カウントダウン中を初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted || isFinished || isCountdownActive)
            return;  // ゴールした後は処理を行わない

        float elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // タイマーを開始
    public void StartTimer()
    {
        if (!isStarted)
        {
            startTime = Time.time;
            isStarted = true;
            isCountdownActive = false;  // カウントダウン終了
        }
    }

    // ゴールに到達したときに呼ばれるメソッド
    public void FinishGame()
    {
        if (!isFinished)
        {
            finishTime = Time.time - startTime;  // 経過時間を記録
            isFinished = true;
        }
    }

    // ゴール時の時間を設定
    public void SetFinishTime(float time)
    {
        finishTime = time;
    }

    // クリアタイムを取得する
    public float GetFinishTime()
    {
        return finishTime;
    }

    // 再プレイ用にリセットするメソッド
    public void ResetTimer()
    {
        startTime = Time.time;
        isFinished = false;
        isStarted = false;
        isCountdownActive = true;  // タイマーリセット時にカウントダウンを有効化

    }
}
