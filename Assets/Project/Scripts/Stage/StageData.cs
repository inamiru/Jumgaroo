using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    public static StageData Instance { get; private set; }

    public float baseTime = 60.0f; // ステージごとの基準タイム

    private void Awake()
    {
        // シングルトンパターンを実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 基準タイムを取得するメソッド
    public float GetBaseTime()
    {
        return baseTime;
    }
}
