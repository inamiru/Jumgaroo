using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    // ScriptableObject のインスタンス
    public PlayerStates playerStates;

    public List<Transform> respawnPoints;        // リスポーンポイントのリスト
    private Transform lastRespawnPoint;             // 最後にいたリスポーンポイント

    private GameOverController gameOverController;

    // Start is called before the first frame update
    void Start()
    {
        gameOverController = GetComponent<GameOverController>();
        lastRespawnPoint = respawnPoints[0];        // 最初のリスポーンポイントを設定

    }

    public void Respawn()
    {
            // 最も近いリスポーンポイントを探す
            Transform nearestRespawnPoint = FindNearestRespawnPoint();

            // プレイヤーをリスポーン地点に移動
            transform.position = nearestRespawnPoint.position;
    }

    private Transform FindNearestRespawnPoint()
    {
        Transform nearestPoint = respawnPoints[0];
        float minDistance = Vector3.Distance(transform.position, nearestPoint.position);

        foreach (var respawnPoint in respawnPoints)
        {
            float distance = Vector3.Distance(transform.position, respawnPoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = respawnPoint;
            }
        }

        return nearestPoint;
    }
}
