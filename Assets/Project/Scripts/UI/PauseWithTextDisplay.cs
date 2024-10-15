using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseWithTextDisplay : MonoBehaviour
{
    // エリアごとの情報を格納する配列
    public AreaInfo[] areaInfos;

    private bool isGamePaused = false;  // ゲームが停止しているかのフラグ

    public PlayerAction playerAction;  // PlayerMovementの参照

    private void Start()
    {
        // 全てのパネルを非表示にしておく
        foreach (var areaInfo in areaInfos)
        {
            areaInfo.panel.SetActive(false);
        }
    }

    // プレイヤーがエリアに入った時に呼ばれるメソッド
    public void OnPlayerEnterArea(int areaID)
    {
        if (areaID >= 0 && areaID < areaInfos.Length)
        {
            // エリアに対応するパネルを表示
            areaInfos[areaID].panel.SetActive(true);

            // ゲームを停止
            PauseGame();
        }
    }

    public void Update()
    {
        // ゲームが停止している時にスペースキーが押されたら再開
        if (isGamePaused && Input.GetKeyDown(KeyCode.Return))
        {
            ResumeGame();  // ゲームを再開
        }
    }

     // ゲームを停止するメソッド
    private void PauseGame()
    {
        isGamePaused = true;
        // ゲーム時間を停止
        Time.timeScale = 0f;

        // プレイヤーのアクションスクリプトを無効化（アクション停止）
        playerAction.DisableInput();
    }

    // ゲームを再開するメソッド
    private void ResumeGame()
    {
        isGamePaused = false;
        // ゲーム時間を再開
        Time.timeScale = 1.0f;

        // 全てのパネルを非表示にする
        foreach (var areaInfo in areaInfos)
        {
            areaInfo.panel.SetActive(false);
        }
        
        // プレイヤーの入力を許可
        playerAction.EnableInput();
    }

// エリアの情報を管理するクラス
[System.Serializable]
public class AreaInfo
{
    public string areaName; // エリア名
    public GameObject panel; // 表示するパネル
}

}
