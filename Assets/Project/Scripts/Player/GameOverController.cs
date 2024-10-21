using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使う場合

namespace TransitionsPlusDemos
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private PlayerAction playerAction;

        private Animator animator;

        public TextMeshProUGUI gameOverText;  // TextMeshProのUIテキスト

        public StageTransition stageTransition;
        public GameObject[] uiElementsToHide;  // 非表示にしたいUI要素

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();

            gameOverText.gameObject.SetActive(false);   // ゲームオーバーテキストを非表示にする
        }

        public void GameOver()
        {
            // 死亡アニメーションの再生
            animator.SetBool("isDead", true);

            // プレイヤーの動きを止める
            StopMovement();

            // UIを非表示にする
            HideUIElements();

            // ゲームオーバーのテキストを表示
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over";

            // ステージ遷移を実行
            if (stageTransition != null)
            {
                stageTransition.StartCoroutine(stageTransition.CallAfterDelayGameOVerTransition());
            }

        }

        // プレイヤーの動きを止める
        private void StopMovement()
        {
            // 例えば、Rigidbodyの速度をゼロにする
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }

            PlayerAction playerAction = GetComponent<PlayerAction>();
            if (playerAction != null)
            {
                playerAction.enabled = false;
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
