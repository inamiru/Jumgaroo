using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScreenManager : MonoBehaviour
{
    public TextMeshProUGUI restartText;      // 「リスタート」のテキスト
    public TextMeshProUGUI stageSelectText;  // 「ステージ選択へ戻る」のテキスト
    public CustomSceneManager customSceneManager;  // カスタムシーンマネージャー
    public ArrowSizeController arrowSizeController;

    private TransitionManager transitionManager;

    private int selectedIndex = 0;  // 0 = リスタート, 1 = ステージ選択（デフォルトは0）

    private bool hasStartedGame = false; // ゲームがスタートしたかどうかのフラグ
    private bool inputLocked = true;    // ユーザー入力をロックするフラグ

    // Start is called before the first frame update
    void Start()
    {
        UpdateSelectionDisplay();  // 初期表示を設定

        transitionManager = FindObjectOfType<TransitionManager>();

        // 初期状態では、両方の矢印は等しいサイズで表示される
        arrowSizeController.ResetArrowScale();
    }

         // Update is called once per frame
    void Update()
    {
        // ResultScreen から結果表示の状態を取得
        ResultScreen resultScreen = FindObjectOfType<ResultScreen>();

        // 結果がすべて表示されている場合のみ入力を受け付ける
        if (resultScreen != null && resultScreen.AreAllResultsDisplayed())
        {
            inputLocked = false;  // 結果が表示されたら入力を受け付ける
        }

        if (inputLocked) return;

        HandleInput();
    }

    // 入力処理
     private void HandleInput()
    {
        // ゲームがすでに始まっている、または入力がロックされている場合は何もしない
        if (hasStartedGame || inputLocked) return;
        
        // 左矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex--;  // インデックスをデクリメント
            if (selectedIndex < 0) // 0未満になったらループ
            {
                selectedIndex = 1; // エンドに戻る
            }
            SoundEffectManager.Instance.PlayArrowKeySound(); // Play arrow key sound
            UpdateSelectionDisplay();
        }

        // 右矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex++;  // インデックスをインクリメント
            if (selectedIndex > 1) // 1を超えたらループ
            {
                selectedIndex = 0; // スタートに戻る
            }
            SoundEffectManager.Instance.PlayArrowKeySound(); // Play arrow key sound
            UpdateSelectionDisplay();
        }
        
        // エンターキーが押された場合
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
        restartText.gameObject.SetActive(selectedIndex == 0);  // 「リスタート」の表示
        stageSelectText.gameObject.SetActive(selectedIndex == 1);  // 「ステージ選択」の表示

        // 矢印のサイズを更新
        arrowSizeController.UpdateArrowSize(selectedIndex);
    }

    // 選択されたアクションを実行
    private void ExecuteSelectedAction()
    {
        if (selectedIndex == 0 && !hasStartedGame)
        {
            hasStartedGame = true;  // ゲームがスタートしたことを記録
            inputLocked = true;      // ユーザー入力をロック
        
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

        Color fadeColor = new Color(0, 0, 0, 1);  // 黒色のフェードアウト
        transitionManager.ExecuteTransition(
            useGradient: false,
            transitionColor: fadeColor,
            sceneNameToLoad: currentSceneName // 選択したステージのシーン名を渡す

        );
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
