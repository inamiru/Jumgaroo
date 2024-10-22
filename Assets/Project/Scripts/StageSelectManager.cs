using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using TransitionsPlus;

namespace TransitionsPlusDemos
{

public class StageSelectManager : MonoBehaviour
{
    public Image thumbnailImage;  // サムネイルを表示するImageコンポーネント
    public TextMeshProUGUI stageNameText;  // ステージ名を表示するTextMeshPro
    public List<StageInfo> stages;  // ステージ情報のリスト

    private int currentStageIndex = 0;  // 現在選択されているステージのインデックス

    public TransitionAnimator animator;

    // Start is called before the first frame update
    void Start()
    {
        // 最初にステージ情報を表示
        UpdateStageDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        // 左矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousStage();
        }

        // 右矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextStage();
        }

        // スペースキーで選択したステージをロード
        if (Input.GetKeyDown(KeyCode.Return))
        {
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
        StageInfo currentStage = stages[currentStageIndex];
        thumbnailImage.sprite = currentStage.thumbnail;  // サムネイル画像を変更
        stageNameText.text = currentStage.stageName;  // ステージ名を変更
    }

    // 選択したステージをロードする
    private void LoadSelectedStage()
    {
        StageInfo selectedStage = stages[currentStageIndex];
                animator.Play();
        SceneManager.LoadScene(selectedStage.sceneName);  // 対応するシーンをロード
    }
}
}
