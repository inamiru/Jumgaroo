using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間
using TransitionsPlus;

namespace TransitionsPlusDemos
{
    public class GoalZone : MonoBehaviour
    {
        public EffectManager effectManager;
        public TransitionAnimator animator;

        public TextMeshProUGUI goalText;  // TextMeshProのUIテキスト
        public string playerTag = "Player";  // プレイヤーのタグ
        public Animator playerAnimator;  // プレイヤーのAnimatorコンポーネント

        public GameObject[] uiElementsToHide;  // 非表示にしたいUI要素

        // Start is called before the first frame update
        void Start()
        {
            // 初期状態ではゴールテキストを非表示に設定
            goalText.enabled = false;

        }

        // プレイヤーが範囲内（トリガー）に入ったら
        void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag(playerTag))
            {
                // UIを非表示にする
                HideUIElements();

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
                animator.Play();
            }
        }

        // UI要素を非表示にする処理
        void HideUIElements()
        {
            foreach (GameObject uiElement in uiElementsToHide)
            {
                if (uiElement != null)
                {
                    uiElement.SetActive(false); // UI要素を非表示にする
                }
            }
        }
    }
}
