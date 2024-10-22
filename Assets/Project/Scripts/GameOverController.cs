using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshProを使う場合
using UnityEngine.SceneManagement;  // シーン管理を行うための名前空間

namespace TransitionsPlusDemos
{
    public class GameOverController : MonoBehaviour
    {
        [SerializeField] private PlayerAction playerAction;
        [SerializeField] private CustomSceneManager customSceneManager;

        private Animator animator;

        public TextMeshProUGUI gameOverText;  // TextMeshProのUIテキスト

        public StageTransition stageTransition;
        public GameObject[] uiElementsToHide;  // 非表示にしたいUI要素

        // Start is called before the first frame update
        void Start()
        {
            // "Kangaroo"という名前のオブジェクトからAnimatorを取得
            GameObject gameOverObject = GameObject.Find("Kangaroo");
            
            animator = gameOverObject.GetComponent<Animator>();

            gameOverText.gameObject.SetActive(false);   // ゲームオーバーテキストを非表示にする
        }

        public void GameOver()
        {
             // ゴール処理
            GameTimeDisplay.Instance.FinishGame();

            // ゴール時の時間を記録
            float finishTime = GameTimeDisplay.Instance.GetFinishTime();
            GameTimeDisplay.Instance.SetFinishTime(finishTime);

            // スコアを記録
            ItemScore.Instance.SetFinalScore();

            // ゲームオーバーのテキストを表示
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over";

            // UIを非表示にする
            HideUIElements();

            // 死亡アニメーションの再生
            animator.SetBool("isDead", true);

            // プレイヤーの動きを止める
            StopMovement();

            EndStage();
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

        // ステージの終了時に呼び出すメソッド
        public void EndStage()
        {
            // 現在のシーン名を取得
            string currentSceneName = SceneManager.GetActiveScene().name;
            
            // 前のシーン名を保存
            customSceneManager.SetPreviousScene(currentSceneName);
    
            // ステージ遷移を実行
            stageTransition.StartCoroutine(stageTransition.CallAfterDelayGameOVerTransition());
        }
    }
}
