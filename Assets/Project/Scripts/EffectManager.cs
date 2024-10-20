using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects;   // エフェクトのプレハブ
    public Transform[] effectPositions;      // 各エフェクトを生成する位置

    public float delayTime = 3.0f;  // エフェクトを表示するまでの待機時間

    public IEnumerator SpawnEffectsAfterDelay()
    {
        // 指定された時間待機
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
