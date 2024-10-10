using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int scoreValue = 10;  // このアイテムが獲得された際に加算するスコア

    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーに接触した場合
        if (other.CompareTag("Player"))
        {
            // スコアを加算
            ScoreManager.instance.AddScore(scoreValue);

            // アイテムを削除
            Destroy(gameObject);
        }
    }
}
