using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageSelectManager : MonoBehaviour
{
    public Image thumbnailImage;  // サムネイルを表示するImageコンポーネント
    public TextMeshProUGUI stageNameText;  // ステージ名を表示するTextMeshPro
    public List<StageInfo> stages;  // ステージ情報のリスト

    private int currentStageIndex = 0;  // 現在選択されているステージのインデックス
    private bool hasStartedGame = false; // ゲームがスタートしたかどうかのフラグ
    private bool inputLocked = false;    // ユーザー入力をロックするフラグ

    public ArrowSizeController arrowSizeController;
    private TransitionManager transitionManager;

    // Start is called before the first frame update
    void Start()
    {
        if (stages.Count > 0)
        {
            UpdateStageDisplay();  
        }

        transitionManager = FindObjectOfType<TransitionManager>();

        // 初期状態では、両方の矢印は等しいサイズで表示される
        arrowSizeController.ResetArrowScale();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    // 入力処理
    private void HandleInput()
    {
        // ゲームが既にスタートしているか、入力がロックされている場合は何もしない
        if (inputLocked || hasStartedGame) return;

        // 左矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundEffectManager.Instance.PlayArrowKeySound(); // Play arrow key sound
            PreviousStage();
        }

        // 右矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundEffectManager.Instance.PlayArrowKeySound(); // Play arrow key sound
            NextStage();
        }

        // エンターキーで選択したステージをロード
        if (Input.GetKeyDown(KeyCode.Return))
        {
            hasStartedGame = true;  // ゲームがスタートしたことを記録
            inputLocked = true;      // ユーザー入力をロック
            
            SoundEffectManager.Instance.PlayReturnKeySound(); // リターンキーのSEを再生
            LoadSelectedStage();
        }
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

        // 矢印のサイズを更新
        arrowSizeController.UpdateArrowSize(currentStageIndex);

    }

    // 選択したステージをロードする
    private void LoadSelectedStage()
    {
        StageInfo selectedStage = stages[currentStageIndex];

        // トランジションを実行し、トランジションが完了した後にシーンをロード
        transitionManager.ExecuteTransition(
            useGradient: true,
            sceneNameToLoad: selectedStage.sceneName // 選択したステージのシーン名を渡す
        );
    }
}
