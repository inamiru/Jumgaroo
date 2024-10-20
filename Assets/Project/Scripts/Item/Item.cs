using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int scoreValue = 10;  // スコアの加算値

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに接触した
        if (other.CompareTag("Player"))
        {
            // アイテムを取得した時にスコアを加算する
            ItemScore.Instance.AddScore(scoreValue);

            // ゲームオブジェクトを消す
            Destroy(gameObject);
        }
    }
}
