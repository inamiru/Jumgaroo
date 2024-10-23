using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TransitionsPlus;

public class StageSelectManager : MonoBehaviour
{
    public Image thumbnailImage;  // サムネイルを表示するImageコンポーネント
    public TextMeshProUGUI stageNameText;  // ステージ名を表示するTextMeshPro
    public List<StageInfo> stages;  // ステージ情報のリスト

    private int currentStageIndex = 0;  // 現在選択されているステージのインデックス
    private bool hasStartedGame = false; // ゲームがスタートしたかどうかのフラグ
    private bool inputLocked = false;    // ユーザー入力をロックするフラグ

    public GameObject leftArrow;        // 左矢印のオブジェクト
    public GameObject rightArrow;       // 右矢印のオブジェクト

    public TransitionAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        if (stages.Count > 0)
        {
            UpdateStageDisplay();  
        }
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
            PreviousStage();
            StartCoroutine(ScaleArrow(leftArrow));  // 左矢印を大きくするコルーチンを開始
        }

        // 右矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextStage();
            StartCoroutine(ScaleArrow(rightArrow));  // 右矢印を大きくするコルーチンを開始
        }

        // エンターキーで選択したステージをロード
        if (Input.GetKeyDown(KeyCode.Return))
        {
            hasStartedGame = true;  // ゲームがスタートしたことを記録
            inputLocked = true;      // ユーザー入力をロック

            LoadSelectedStage();
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

    // ステージを1つ前に切り替える
    private void PreviousStage()
    {
        currentStageIndex--;
        if (currentStageIndex < 0)
        {
            currentStageIndex = stages.Count - 1;  // リストの最後に戻る
        }
        UpdateStageDisplay();
    }

    // ステージを1つ後に切り替える
    private void NextStage()
    {
        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            currentStageIndex = 0;  // リストの最初に戻る
        }
        UpdateStageDisplay();
    }

    // 選択されているステージを表示する
    private void UpdateStageDisplay()
    {
        if (stages.Count == 0) return; // ステージがない場合は何もしない

        StageInfo currentStage = stages[currentStageIndex];
        thumbnailImage.sprite = currentStage.thumbnail;  // サムネイル画像を変更
        stageNameText.text = currentStage.stageName;  // ステージ名を変更
    }

    // 選択したステージをロードする
    private void LoadSelectedStage()
    {
        StageInfo selectedStage = stages[currentStageIndex];

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
            keepAspectRatio : true,
            sceneNameToLoad: selectedStage.sceneName // ロードするシーン名
        );
    }
}
