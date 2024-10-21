using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TransitionsPlusDemos;

    public class ResultScreenManager : MonoBehaviour
    {

        public TextMeshProUGUI selectionText;  // テキストUIの参照
        public SelectionManager selectionManager;  // SelectionManagerの参照
        public CustomSceneManager custtomSceneManager;  // CustomSceneManagerの参照
        
        public float displayDelay = 1.0f;  // 結果表示の遅延時間

        // Start is called before the first frame update
        void Start()
        {
        // 初期のテキスト表示
        UpdateSelectionText();
        StartCoroutine(WaitAndEnableInput());
        }

    // 一定時間後に操作可能にする
    private IEnumerator WaitAndEnableInput()
    {
        yield return new WaitForSeconds(displayDelay);
        StartCoroutine(HandleInput());
    }

    // ユーザー入力を処理する
    private IEnumerator HandleInput()
    {
        while (true)
        {
            // スペースキーで選択されたアクションを実行する
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteAction();
                break;
            }

            // 選択肢のテキストを更新
            UpdateSelectionText();
            yield return null;
        }
    }

    // 選択されたテキストをUIに表示
    private void UpdateSelectionText()
    {
        selectionText.text = selectionManager.CurrentSelectionText;
    }

    // 選択されたアクションを実行する
    private void ExecuteAction()
    {
        switch (selectionManager.GetCurrentSelection())
        {
            case SelectionManager.Selection.Restart:
                custtomSceneManager.RestartCurrentScene();  // リスタート処理
                break;
            case SelectionManager.Selection.StageSelect:
                custtomSceneManager.LoadScene("StageSelectScene");  // ステージ選択シーンに遷移
                break;
        }
    }
}
