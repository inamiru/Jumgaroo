using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


    public class ResultScreenManager : MonoBehaviour
    {
        public TextMeshProUGUI restartText;      // 「リスタート」のテキスト
        public TextMeshProUGUI stageSelectText;  // 「ステージ選択へ戻る」のテキスト
        public CustomSceneManager customSceneManager;  // カスタムシーンマネージャー

        private int selectedIndex = 0;  // 0 = リスタート, 1 = ステージ選択（デフォルトは0）

        // Start is called before the first frame update
        void Start()
        {
            UpdateSelectionDisplay();  // 初期表示を設定
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

        // 現在のステージをリスタートする
        private void RestartStage()
        {
            string currentSceneName = StageController.instance.GetCurrentScene(); // 現在のシーン名を取得

            if (!string.IsNullOrEmpty(currentSceneName)) // 空でないか確認
            {
                customSceneManager.LoadScene(currentSceneName);  // 現在のステージを再スタート
            }
            else
            {
                Debug.LogWarning("Current scene name is empty!"); // 警告メッセージ
            }
        }

        // ステージ選択画面に移動
        private void LoadStageSelectScene()
        {
            customSceneManager.LoadStageSelectScene();  // ステージ選択シーンに遷移
        }
    }
