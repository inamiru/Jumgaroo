using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public List<Transform> respawnPoints;        // リスポーンポイントのリスト
    private Transform lastRespawnPoint;             // 最後にいたリスポーンポイント


    PlayerController playerController;
    TakeDamage takeDamage;

    void Start ()
    {
        playerController = GetComponent<PlayerController>();
        takeDamage = GetComponent<TakeDamage>();

        lastRespawnPoint = respawnPoints[0];        // 最初のリスポーンポイントを設定
    }

    public void Respawn()
    {
        if (playerController.currentHitCount != 0)
        {
            Transform closestRespawnPoint = GetClosestRespawnPoint(); // 最も近いリスポーンポイントを取得
            
            if (closestRespawnPoint != null) // リスポーンポイントが見つかった場合
            {
                transform.position = closestRespawnPoint.position; // リスポーンポイントに移動
                Debug.Log("Player Respawned at: " + closestRespawnPoint.position); // リスポーン時のデバッグメッセージ
            }
        }

        // 接触回数が0になったらゲームオーバー処理を呼び出し
        if (playerController.currentHitCount <= 0)
        {
            takeDamage.GameOver(); // ゲームオーバー処理を呼び出す
        }
    }

    private Transform GetClosestRespawnPoint()
    {
        Transform closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform point in respawnPoints)
        {
            float distance = Vector3.Distance(transform.position, point.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }
        return closestPoint; // 最も近いリスポーンポイントを返す
    }
}
