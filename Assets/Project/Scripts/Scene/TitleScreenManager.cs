using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleScreenManager : MonoBehaviour
{
    public TextMeshProUGUI startText;  // ゲームスタートのテキスト
    public TextMeshProUGUI exitText;   // エンドのテキスト

    private int selectedIndex = 0;      // 現在選択されている項目（0 = スタート, 1 = エンド）
    private bool inputLocked = false;   // ユーザー入力をロックするフラグ

    public ArrowSizeController arrowSizeController;
    private TransitionManager transitionManager;


    // Start is called before the first frame update
    void Start()
    {
        transitionManager = FindObjectOfType<TransitionManager>();

        UpdateSelectionDisplay();      // 初期選択状態を表示
        arrowSizeController.ResetArrowScale();  // 初期状態の矢印サイズ設定

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

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = (selectedIndex + 1) % 2;  // 左に移動
            SoundEffectManager.Instance.PlayArrowKeySound(); // Play arrow key sound
            UpdateSelectionDisplay();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = (selectedIndex + 2 - 1) % 2;  // 右に移動
            SoundEffectManager.Instance.PlayArrowKeySound(); // Play arrow key sound
            UpdateSelectionDisplay();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SoundEffectManager.Instance.PlayReturnKeySound(); // リターンキーのSEを再生
            ExecuteSelectedAction();
        }
    }

    // 選択されたテキストの表示を更新（表示と非表示の切り替え）
    private void UpdateSelectionDisplay()
    {
        // 選択された項目のみ表示
        startText.gameObject.SetActive(selectedIndex == 0);  // 「リスタート」の表示
        exitText.gameObject.SetActive(selectedIndex == 1);   // 「ステージ選択」の表示
        
        // 矢印のサイズを更新
        arrowSizeController.UpdateArrowSize(selectedIndex);
    }

    // 選択されたアクションを実行
    private void ExecuteSelectedAction()
    {
        if (selectedIndex == 0)
        {
            if (!inputLocked && transitionManager != null)
            {
                LoadStageSelectScene();
                inputLocked = true;  // ユーザー入力をロック
            }
        }
        else if (selectedIndex == 1)
        {
            Application.Quit();  // ゲームを終了

            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;  // エディタ上での終了動作
            #endif
        }
    }

    // ステージ選択画面に移動
    private void LoadStageSelectScene()
    {
        // トランジションを実行し、トランジションが完了した後にシーンをロード
        transitionManager.ExecuteTransition(
            useGradient: true,
            sceneNameToLoad: "StageSelectScene" // 選択したステージのシーン名を渡す
        );        
    }
}
