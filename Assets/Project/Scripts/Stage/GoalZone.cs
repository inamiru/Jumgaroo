using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使用するための名前空間

namespace TransitionsPlusDemos
{
    public class GoalZone : MonoBehaviour
    {
        [SerializeField] private CustomSceneManager customSceneManager;

        public StageClearEffects stageClearEffects;
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

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                BGMSoundManager.Instance.StopBGM();
                BGMSoundManager.Instance.PlayGameClearBGM();

                GameTimeDisplay.Instance.FinishGame();
                float finishTime = GameTimeDisplay.Instance.GetFinishTime();
                GameTimeDisplay.Instance.SetFinishTime(finishTime);
            
                ItemScore.Instance.SetFinalScore();

                HideUIElements();

                goalText.enabled = true;
                StartCoroutine(stageClearEffects.SpawnEffectsAfterDelay());

                playerAnimator.SetTrigger("GoalReached");

                PlayerAction playerActionScript = other.GetComponent<PlayerAction>();
                PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
                PlayerJump playerJump = other.GetComponent<PlayerJump>();

                Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

                if (playerActionScript != null)
                {
                    playerActionScript.DisableInput();  // 移動スクリプトを無効化し、移動も停止
                }

                if (playerMovement != null)
                {
                    playerMovement.StopMovement();
                }

                if (playerJump != null)
                {
                    playerJump.DisableInput(); // ジャンプを無効にする
                }

                // Rigidbodyがある場合、物理演算を無効化して強制的に停止させる
                if (playerRigidbody != null)
                {
                    playerRigidbody.velocity = Vector3.zero;
                }

                if (stageTransition != null)
                {
                    StartCoroutine(stageTransition.CallAfterDelayStageClearTransition());
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
