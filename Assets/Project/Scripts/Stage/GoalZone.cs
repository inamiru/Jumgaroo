using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間

public class GoalZone : MonoBehaviour
{
    public TextMeshProUGUI goalText;  // TextMeshProのUIテキスト
    public string playerTag = "Player";  // プレイヤーのタグ
    private bool goalReached = false;  // ゴールに到達したかのフラグ
    public Animator playerAnimator;  // プレイヤーのAnimatorコンポーネント

    public GameObject[] effects; // 複数のエフェクトのプレハブを格納する配列
    public Transform[] effectPositions; // エフェクトを表示する位置の配列

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

            // ゴールに接触した際にエフェクトを表示する
            DisplayEffects();

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

    // エフェクトをワールド座標で表示する処理
    private void DisplayEffects()
    {
        for (int i = 0; i < effects.Length; i++)
        {
            if (i < effectPositions.Length)
            {
                // 親を指定せずにワールド座標でエフェクトを生成する
                //Instantiate(effects[i], effectPositions[i].position, effectPositions[i].rotation);

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
