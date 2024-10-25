using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;  // シングルトンインスタンス
    public GameObject dustEffectPrefab;
    public GameObject heartLostEffectPrefab;
    private float defaultEffectLifetime = 1.0f; // デフォルトのエフェクト生存時間

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // エフェクトプレハブが設定されているか確認
        if (dustEffectPrefab == null || heartLostEffectPrefab == null)
        {
            Debug.LogWarning("EffectManager: 一部のエフェクトプレハブが設定されていません");
        }
    }

    // ほこりエフェクトを指定した位置で再生するメソッド
    public void PlayDustEffect(Vector3 position, float lifetime = -1f)
    {
        if (dustEffectPrefab != null)
        {
            var dustEffect = Instantiate(dustEffectPrefab, position, Quaternion.identity);
            Destroy(dustEffect, lifetime > 0 ? lifetime : defaultEffectLifetime);
        }
    }

    // ハート喪失エフェクトを指定した位置で再生するメソッド
    public void PlayHeartLostEffect(Vector3 position)
    {
        if (heartLostEffectPrefab != null)
        {
            GameObject heartLostEffect = Instantiate(heartLostEffectPrefab, position, Quaternion.identity);
            Destroy(heartLostEffect, 1.0f);
        }
    }
}