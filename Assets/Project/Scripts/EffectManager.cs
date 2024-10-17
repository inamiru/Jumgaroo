using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects; // 複数のエフェクトのプレハブを格納する配列
    public Transform[] effectPositions; // エフェクトを表示する位置の配列

    public float delayTime = 3.0f;  // エフェクトを再生するまでの遅延時間

    void Start()
    {
        // 指定した時間後にエフェクトを再生するコルーチンを開始
        StartCoroutine(SpawnEffectsAfterDelay());
    }

    public IEnumerator SpawnEffectsAfterDelay()
    {
        // 指定した秒数待機
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < effects.Length; i++)
        {
            if (i < effectPositions.Length)
            {
                // エフェクトを生成した後に、その位置を固定する
                GameObject effect = Instantiate(effects[i], effectPositions[i].position, effectPositions[i].rotation);
                effect.transform.position = effectPositions[i].position; // 位置を固定

            }
            else
            {
                // 位置が足りない場合はゴールエリアのワールド座標に生成
                Instantiate(effects[i], transform.position, Quaternion.identity);
            }
        }

    }
}
