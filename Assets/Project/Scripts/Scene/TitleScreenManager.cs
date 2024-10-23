using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TransitionsPlus;

public class TitleScreenManager : MonoBehaviour
{
    public TextMeshProUGUI startText;  // ゲームスタートのテキスト
    public TextMeshProUGUI exitText;   // エンドのテキスト

    private int selectedIndex = 0;      // 現在選択されている項目（0 = スタート, 1 = エンド）
    private bool hasStartedGame = false; // ゲームがスタートしたかどうかのフラグ
    private bool inputLocked = false;    // ユーザー入力をロックするフラグ


    public TransitionAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        UpdateSelectionDisplay();  // 初期選択状態を表示
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    // 入力処理
    private void HandleInput()
    {
        // 入力がロックされている場合は何もしない
        if (inputLocked) return;
        
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
        startText.gameObject.SetActive(selectedIndex == 0);  // 「リスタート」の表示
        exitText.gameObject.SetActive(selectedIndex == 1);  // 「ステージ選択」の表示
    }

    // 選択されたアクションを実行
    private void ExecuteSelectedAction()
    {
        if (selectedIndex == 0 && !hasStartedGame)
        {
            animator.Play();
            hasStartedGame = true;  // ゲームがスタートしたことを記録
            inputLocked = true;      // ユーザー入力をロック
        }
        else if (selectedIndex == 1)
        {
            // エンドを選択 -> ゲームを終了
            Application.Quit();  // ゲームを終了
            Debug.Log("Game is quitting..."); // デバッグ用メッセージ
        }
    }
}
