using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public enum Selection { Restart, StageSelect }
    public Selection currentSelection = Selection.Restart;  // 初期選択は「リスタート」

    public string restartText = "Restart";
    public string StageSelectText = "Stage Select";

    public string CurrentSelectionText
    {
        get
        {
            return currentSelection == Selection.Restart ? restartText : StageSelectText;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 左右の矢印キーで選択を切り替え
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentSelection = Selection.Restart;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentSelection = Selection.StageSelect;
        }
    }

    // 現在の選択を取得するメソッド
    public Selection GetCurrentSelection()
    {
        return currentSelection;
    }
}
