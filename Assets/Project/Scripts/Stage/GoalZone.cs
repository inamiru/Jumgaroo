using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間

public class GoalZone : MonoBehaviour
{
    public EffectManager effectManager;

    public TextMeshProUGUI goalText;  // TextMeshProのUIテキスト
    public string playerTag = "Player";  // プレイヤーのタグ
    private bool goalReached = false;  // ゴールに到達したかのフラグ
    public Animator playerAnimator;  // プレイヤーのAnimatorコンポーネント



    // Start is called before the first frame update
    void Start()
    {
        // 初期状態ではゴールテキストを非表示に設定
        goalText.enabled = false;
    }

    // プレイヤーが範囲内（トリガー）に入ったら
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !goalReached)
        {
            // ゴールに到達したフラグを立てる
            goalReached = true;

            // ゴールテキストを表示
            goalText.enabled = true;

            // エフェクトを表示するコルーチンを開始
            effectManager.StartCoroutine(effectManager.SpawnEffectsAfterDelay());

            // プレイヤーのアニメーションをゴールに到達したものに切り替える
            playerAnimator.SetTrigger("GoalReached");

            // プレイヤーのRigidbodyの動きを止める
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // プレイヤーの速度をゼロにする
                playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;

                // プレイヤーの入力を無効にする（プレイヤーの移動スクリプトを無効にする）
                PlayerAction playerActionScript = other.GetComponent<PlayerAction>();
                if (playerActionScript != null)
                {
                    playerActionScript.enabled = false;  // 移動スクリプトを無効化
                }
            }
        }
    }
}
