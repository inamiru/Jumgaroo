using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TransitionsPlusDemos
{
    public class ResultScreenManager : MonoBehaviour
    {
        public TextMeshProUGUI restartText;      // 「リスタート」のテキスト
        public TextMeshProUGUI stageSelectText;  // 「ステージ選択へ戻る」のテキスト
        public CustomSceneManager customSceneManager;  // カスタムシーンマネージャー

        private int selectedIndex = 0;  // 0 = リスタート, 1 = ステージ選択（デフォルトは0）
        private string previousStageName;  // 直前に遊んでいたステージ名

        // Start is called before the first frame update
        void Start()
        {
            // カスタムシーンマネージャーから直前のステージ名を取得
            previousStageName = customSceneManager.GetPreviousSceneName();
            // 初期表示を「リスタート」に設定
            UpdateSelectionDisplay();
        }

         // Update is called once per frame
        void Update()
        {
            HandleInput();
        }

        // 入力処理
        private void HandleInput()
        {
            // 左矢印キーまたは右矢印キーが押された場合
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                selectedIndex = 1 - selectedIndex;  // 0と1を切り替える
                UpdateSelectionDisplay();
            }

            // エンターキーが押された場合
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ExecuteSelectedAction();
            }
        }

        // 選択されたテキストの表示を更新（表示と非表示の切り替え）
        private void UpdateSelectionDisplay()
        {
            // 選択された項目のみ表示
            restartText.gameObject.SetActive(selectedIndex == 0);  // 「リスタート」の表示
            stageSelectText.gameObject.SetActive(selectedIndex == 1);  // 「ステージ選択」の表示
        }

        // 選択されたアクションを実行
        private void ExecuteSelectedAction()
        {
            if (selectedIndex == 0)
            {
                // リスタートを選択した場合 -> 現在のステージを再スタート
                RestartStage();
            }
            else
            {
                // ステージ選択に戻るを選択した場合 -> ステージ選択画面に遷移
                LoadStageSelectScene();
            }
        }

        // 直前に遊んでいたステージをリスタートする
        private void RestartStage()
        {
                customSceneManager.LoadScene(previousStageName);  // 前のステージを最初から再スタート
        }

        // ステージ選択画面に移動
        private void LoadStageSelectScene()
        {
            customSceneManager.LoadStageSelectScene();  // ステージ選択シーンに遷移
        }
    }
}
