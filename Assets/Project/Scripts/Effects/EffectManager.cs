using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;  // EffectManagerのシングルトンインスタンス

    public GameObject dustEffectPrefab;    // ほこりエフェクトのプレハブ
    public GameObject heartLostEffectPrefab; // ハート喪失エフェクトのプレハブ

    private void Awake()
    {
        // シングルトンの初期化
        if (instance == null)
        {
            instance = this;  // 現在のインスタンスをシングルトンインスタンスとして設定
            DontDestroyOnLoad(gameObject);  // シーンが変わってもこのオブジェクトを破棄しない
        }
        else
        {
            Destroy(gameObject);  // 既にインスタンスが存在する場合、重複したオブジェクトを破棄
        }
    }

    // ほこりエフェクトを指定した位置で再生するメソッド
    public void PlayDustEffect(Vector3 position)
    {
        Instantiate(dustEffectPrefab, position, Quaternion.identity); // 指定位置にほこりエフェクトを生成
    }

    // ハート喪失エフェクトを指定した位置で再生するメソッド
    public void PlayHeartLostEffect(Vector3 position)
    {
        Instantiate(heartLostEffectPrefab, position, Quaternion.identity); // 指定位置にハート喪失エフェクトを生成
    }
}