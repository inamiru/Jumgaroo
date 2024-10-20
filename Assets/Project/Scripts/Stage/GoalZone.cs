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
        public StageTransition stageTransition;

        public TextMeshProUGUI goalText;  // TextMeshProのUIテキスト

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
        if (other.CompareTag("Player"))
        {
             // ゴール処理
            GameTimeDisplay.Instance.FinishGame();  // ゴール処理を呼ぶ

            // ゴール時の時間を記録
            float finishTime = GameTimeDisplay.Instance.GetFinishTime();
            GameTimeDisplay.Instance.SetFinishTime(finishTime);

            // スコアを記録
            ItemScore.Instance.SetFinalScore();

            // UIを非表示にする
            HideUIElements();

            // ゴールテキストを表示
            goalText.enabled = true;

            // エフェクトを表示するコルーチンを開始
            effectManager.StartCoroutine(effectManager.SpawnEffectsAfterDelay());

            // プレイヤーのアニメーションをゴールに到達したものに切り替える
            playerAnimator.SetTrigger("GoalReached");

            // プレイヤーの移動スクリプトを無効化
            PlayerAction playerActionScript = other.GetComponent<PlayerAction>();

            if (playerActionScript != null)
            {
                playerActionScript.enabled = false;  // 移動スクリプトを無効化
            }

            // ステージ遷移を実行
            if (stageTransition != null)
            {
                stageTransition.StartCoroutine(stageTransition.CallAfterDelayTransition());
            }
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
