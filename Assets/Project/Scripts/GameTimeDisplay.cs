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
        startTime = Time.time;  // ゲーム開始時刻を保存
        isFinished = false;     // ゲームが終了していない状態を設定
    }

    // Update is called once per frame
    void Update()
    {
        if (isFinished)
            return;  // ゴールした後は処理を行わない

        // 現在の経過時間を計算
        float elapsedTime = Time.time - startTime;

        // 経過時間を分・秒・ミリ秒の形式に変換
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);

        // 経過時間をテキストに反映
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
        Debug.Log("Timer Reset. startTime: " + startTime);  // リセットしたタイムを表示
    }
}
