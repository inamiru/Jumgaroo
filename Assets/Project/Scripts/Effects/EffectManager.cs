using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager _instance;  // シングルトンインスタンス
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
    private float defaultEffectLifetime = 1.0f; // デフォルトのエフェクト生存時間

    private Queue<GameObject> dustEffectPool = new Queue<GameObject>();
    private Queue<GameObject> heartLostEffectPool = new Queue<GameObject>();

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
        GameObject dustEffect = GetEffectFromPool(dustEffectPool, dustEffectPrefab);
        if (dustEffect != null)
        {
            dustEffect.transform.position = position;
            dustEffect.SetActive(true);

            StartCoroutine(DeactivateEffectAfterTime(dustEffect, dustEffectPool, lifetime > 0 ? lifetime : defaultEffectLifetime));
        }
    }

    // ハート喪失エフェクトを指定した位置で再生するメソッド
    public void PlayHeartLostEffects(Vector3[] positions)
    {
        foreach (var position in positions)
        {
            PlayHeartLostEffect(position);
        }
    }

     // エフェクトをプールから取得し、必要なら新しく生成する
    private GameObject GetEffectFromPool(Queue<GameObject> pool, GameObject prefab)
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }
        else if (prefab != null)
        {
            return Instantiate(prefab);
        }
        return null;
    }

    // エフェクトを非アクティブにしてプールに戻すコルーチン
    private IEnumerator DeactivateEffectAfterTime(GameObject effect, Queue<GameObject> pool, float delay)
    {
        yield return new WaitForSeconds(delay);
        effect.SetActive(false);
        pool.Enqueue(effect);
    }

    // ハート喪失エフェクトを再生するためのメソッド
    private void PlayHeartLostEffect(Vector3 position)
    {
        GameObject heartLostEffect = GetEffectFromPool(heartLostEffectPool, heartLostEffectPrefab);
        if (heartLostEffect != null)
        {
            heartLostEffect.transform.position = position;
            heartLostEffect.SetActive(true);
            StartCoroutine(DeactivateEffectAfterTime(heartLostEffect, heartLostEffectPool, defaultEffectLifetime));
        }
    }
}