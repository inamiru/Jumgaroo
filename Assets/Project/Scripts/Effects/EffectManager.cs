using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;  // シングルトンインスタンス

    public GameObject dustEffectPrefab;    // 歩く時の土煙エフェクトのプリファブ
    public GameObject heartLostEffectPrefab; // ハートが減ったときのエフェクトのプリファブ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // シーン間でオブジェクトが破棄されないようにする
        }
        else
        {
            Destroy(gameObject);  // 他のインスタンスが存在する場合は、新しいオブジェクトを破棄
        }
    }

    // 土煙エフェクトを再生するメソッド
    public void PlayDustEffect(Vector3 position)
    {
        Instantiate(dustEffectPrefab, position, Quaternion.identity);
    }

    // ハートが減ったときのエフェクトを再生するメソッド
    public void PlayHeartLostEffect(Vector3 position)
    {
        Instantiate(heartLostEffectPrefab, position, Quaternion.identity);
    }
}
