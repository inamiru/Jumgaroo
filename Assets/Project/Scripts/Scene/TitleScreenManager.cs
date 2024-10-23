using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TransitionsPlus;

public class TitleScreenManager : MonoBehaviour
{
    public TextMeshProUGUI startText;  // ゲームスタートのテキスト
    public TextMeshProUGUI exitText;   // エンドのテキスト
    public GameObject leftArrow;        // 左矢印のオブジェクト
    public GameObject rightArrow;       // 右矢印のオブジェクト

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
        
        // 左矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex--;  // インデックスをデクリメント
            if (selectedIndex < 0) // 0未満になったらループ
            {
                selectedIndex = 1; // エンドに戻る
            }
            UpdateSelectionDisplay();
            StartCoroutine(ScaleArrow(leftArrow));  // 左矢印を大きくするコルーチンを開始
        }

        // 右矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex++;  // インデックスをインクリメント
            if (selectedIndex > 1) // 1を超えたらループ
            {
                selectedIndex = 0; // スタートに戻る
            }
            UpdateSelectionDisplay();
            StartCoroutine(ScaleArrow(rightArrow));  // 右矢印を大きくするコルーチンを開始
        }
        
        // エンターキーが押された場合
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteSelectedAction();
        }
    }

    // 矢印を大きくするコルーチン
    private IEnumerator ScaleArrow(GameObject arrow)
    {
        Vector3 originalScale = arrow.transform.localScale; // 元のスケールを保存
        arrow.transform.localScale = originalScale * 1.5f; // 矢印を大きくする
        yield return new WaitForSeconds(0.1f); // 一瞬待つ
        arrow.transform.localScale = originalScale; // 元のスケールに戻す
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
