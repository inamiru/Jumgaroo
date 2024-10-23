using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TransitionsPlus;


public class ResultScreenManager : MonoBehaviour
{
    public TextMeshProUGUI restartText;      // 「リスタート」のテキスト
    public TextMeshProUGUI stageSelectText;  // 「ステージ選択へ戻る」のテキスト
    public CustomSceneManager customSceneManager;  // カスタムシーンマネージャー

    public TransitionAnimator animator;

    private int selectedIndex = 0;  // 0 = リスタート, 1 = ステージ選択（デフォルトは0）

    private bool hasStartedGame = false; // ゲームがスタートしたかどうかのフラグ
    private bool inputLocked = false;    // ユーザー入力をロックするフラグ

    public GameObject leftArrow;        // 左矢印のオブジェクト
    public GameObject rightArrow;       // 右矢印のオブジェクト

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
        restartText.gameObject.SetActive(selectedIndex == 0);  // 「リスタート」の表示
        stageSelectText.gameObject.SetActive(selectedIndex == 1);  // 「ステージ選択」の表示
    }

    // 選択されたアクションを実行
    private void ExecuteSelectedAction()
    {
        if (selectedIndex == 0 && !hasStartedGame)
        {
            hasStartedGame = true;  // ゲームがスタートしたことを記録
            inputLocked = true;      // ユーザー入力をロック
        
        Color transitionColor = new Color(0, 0, 0, 1);  // 任意の色を設定
       
        // トランジションを開始
        TransitionAnimator.Start(
            TransitionType.Shape,     // transition type
            duration: 2.0f,            // transition duration in seconds
            rotationMultiplier: -2,
            splits: 2,
            color : transitionColor,
            keepAspectRatio : true
        );
            // リスタートを選択した場合 -> 現在のステージを再スタート
            RestartStage();
        }
        else
        {
                    Gradient gradient = new Gradient();

        // 16進数カラーコードからColorを生成（#RRGGBBAA形式）
        Color colorStart;
        Color colorEnd;

        ColorUtility.TryParseHtmlString("#F8732B", out colorStart);
        ColorUtility.TryParseHtmlString("#D6B436", out colorEnd);

        // GradientColorKey配列を作成し、Colorと時間を指定
        gradient.colorKeys = new GradientColorKey[] {
            new GradientColorKey(colorStart, 0.0f),  // 最初の色（赤）
            new GradientColorKey(colorEnd, 1.0f)     // 最後の色（半透明の青）
        };

        // トランジションを開始
        TransitionAnimator.Start(
            TransitionType.Shape,     // transition type
            duration: 2.0f,            // transition duration in seconds
            rotationMultiplier: -2,
            splits: 2,
            gradient : gradient,
            keepAspectRatio : true
        );
        
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
    }

    // ステージ選択画面に移動
    private void LoadStageSelectScene()
    {
        customSceneManager.LoadStageSelectScene();  // ステージ選択シーンに遷移
        
    }
}
