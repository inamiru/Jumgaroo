using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理を行うための名前空間

public class CustomSceneManager : MonoBehaviour
{
    private string previousSceneName;  // 前のシーン名を保存
    private string currentSceneName;   // 現在のシーン名を保存

    // ステージ選択シーンの名前を定数で管理
    private const string stageSelectSceneName = "StageSelectScene";

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
        previousSceneName = ""; // 初期化
    }

    public void SetPreviousScene(string sceneName)
    {
        previousSceneName = sceneName;
    }

    // 前のシーンの名前を取得
    public string GetPreviousSceneName()
    {
        return previousSceneName;
    }

    // 現在のシーンを再読み込み（リスタート）
    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(currentSceneName);
    }

    // 前のシーンを再読み込み（リスタート）
    public void RestartPreviousScene()
    {
        if (!string.IsNullOrEmpty(previousSceneName))
        {
            SceneManager.LoadScene(previousSceneName);  // 前のシーンをロード
        }
    }

    // 特定のシーンをロード
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("Scene name is empty or null."); // 警告メッセージ
            return;
        }

        SetPreviousScene(SceneManager.GetActiveScene().name);  // 現在のシーンを保存
        SceneManager.LoadScene(sceneName);   // 指定されたシーンをロード
    }

    // ステージ選択画面をロード
    public void LoadStageSelectScene()
    {
        LoadScene(stageSelectSceneName);
    }
}
