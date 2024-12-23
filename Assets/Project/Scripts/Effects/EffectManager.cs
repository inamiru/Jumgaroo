using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;
    public static EffectManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EffectManager>();
            }
            return _instance;
        }
    }

    public GameObject dustEffectPrefab;
    public GameObject heartLostEffectPrefab;
    public GameObject itemCollectEffectPrefab;
    public GameObject jumpEffectPrefab;  // ジャンプエフェクトのプレハブ
    public GameObject starEffectPrefab;

    private float defaultEffectLifetime = 1.0f;

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

    // ほこりエフェクトを指定した位置で再生するメソッド
    public void PlayDustEffect(Vector3 position, float lifetime = -1f)
    {
        GameObject dustEffect = Instantiate(dustEffectPrefab, position, Quaternion.identity);
        StartCoroutine(DestroyEffectAfterTime(dustEffect, lifetime > 0 ? lifetime : defaultEffectLifetime));
    }

    public void PlayJumpEffect(Vector3 position)
    {
        if (jumpEffectPrefab != null)
        {
            GameObject jumpEffect = Instantiate(jumpEffectPrefab, position, Quaternion.identity);
            Destroy(jumpEffect, 1.0f);  // 1秒後にエフェクトを削除
        }
    }

    // ハート喪失エフェクトを1つ再生するメソッド
    public void PlayHeartLostEffect(Vector3 position)
    {
        GameObject heartLostEffect = Instantiate(heartLostEffectPrefab, position, Quaternion.identity);
        StartCoroutine(DestroyEffectAfterTime(heartLostEffect, defaultEffectLifetime));
    }
    
    // エフェクトを指定時間後に削除するコルーチン
    private IEnumerator DestroyEffectAfterTime(GameObject effect, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(effect);
    }

    // アイテム取得エフェクトを表示するメソッド
    public void PlayItemCollectEffect(Vector3 position)
    {
        if (itemCollectEffectPrefab != null)
        {
            GameObject effect = Instantiate(itemCollectEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1.0f);  // エフェクトを一定時間後に破棄する
        }
    }

    public void PlayStarEffect(Vector3 position)
    {
        Instantiate(starEffectPrefab, position, Quaternion.identity);
    }
}