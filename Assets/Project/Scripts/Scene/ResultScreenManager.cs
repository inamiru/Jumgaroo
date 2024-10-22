using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TransitionsPlusDemos;

public class ResultScreenManager : MonoBehaviour
{
    public CustomSceneManager sceneManager;  // CustomSceneManagerの参照

    public TextMeshProUGUI optionText;  // メニューのテキスト表示
    private enum MenuOption { Restart, StageSelect }
    private MenuOption currentOption = MenuOption.Restart;

    void Start()
    {
        UpdateOptionText();  // 初期の選択肢を表示
    }

    void Update()
    {
        HandleInput();  // ユーザー入力を処理
    }

    private void HandleInput()
    {
        // 左右の矢印キーでメニューを切り替え
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ToggleOption();
        }

        // スペースキーで選択されたアクションを実行
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteAction();
        }
    }

    // 現在の選択肢を切り替え
    private void ToggleOption()
    {
        if (currentOption == MenuOption.Restart)
        {
            currentOption = MenuOption.StageSelect;
        }
        else
        {
            currentOption = MenuOption.Restart;
        }

        UpdateOptionText();  // テキストを更新
    }

    // 選択肢に応じてテキストを更新
    private void UpdateOptionText()
    {
        if (currentOption == MenuOption.Restart)
        {
            optionText.text = "Restart";
        }
        else
        {
            optionText.text = "Stage Select";
        }
    }

    // 選択されたアクションを実行
    private void ExecuteAction()
    {
        if (currentOption == MenuOption.Restart)
        {
            sceneManager.RestartLastPlayedStage();  // 直前のステージをリスタート
        }
        else if (currentOption == MenuOption.StageSelect)
        {
            sceneManager.LoadStageSelectScene();  // ステージ選択画面に遷移
        }
    }
}
