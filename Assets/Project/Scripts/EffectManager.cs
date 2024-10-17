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

    }

    public IEnumerator SpawnEffectsAfterDelay()
    {
        // 指定した秒数待機
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < effects.Length; i++)
        {
            if (i < effectPositions.Length)
            {
                // エフェクトを生成
                GameObject effect = Instantiate(effects[i], effectPositions[i].position, effectPositions[i].rotation);

            }
        }

    }
}
