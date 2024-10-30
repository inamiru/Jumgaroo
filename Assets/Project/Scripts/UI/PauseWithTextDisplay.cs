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
    private bool inputSuppressed = false;  // 入力抑制フラグ

    public PlayerAction playerAction;  // PlayerMovementの参照
    public PlayerJump playerJump;

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
            SoundEffectManager.Instance?.PlayOpenPanelSound();
            areaInfos[areaID].panel.SetActive(true);
            PauseGame();
        }
    }

    public void Update()
    {
        if (isGamePaused && Input.GetKeyDown(KeyCode.Return))
        {
            SoundEffectManager.Instance.PlayReturnKeySound();
            ResumeGame();
        }
    }

    // ゲームを停止するメソッド
    private void PauseGame()
    {
        isGamePaused = true;
        inputSuppressed = true;
        Time.timeScale = 0f;
        playerAction.DisableInput();
        playerJump.DisableInput();
    }

    // ゲームを再開するメソッド
    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;

        foreach (var areaInfo in areaInfos)
        {
            areaInfo.panel.SetActive(false);
        }

        inputSuppressed = false;
        playerJump.ClearJumpBuffer();
        playerJump.EnableInput();
    }

    // エリアの情報を管理するクラス
    [System.Serializable]
    public class AreaInfo
    {
        public string areaName; // エリア名
        public GameObject panel; // 表示するパネル
    }

}
