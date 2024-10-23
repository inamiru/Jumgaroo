using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    public static StageController instance;  // Singleton インスタンス
    private string currentSceneName;         // 現在のシーン名を保存

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;  // シングルトンインスタンスの設定
            DontDestroyOnLoad(gameObject);  // シーンを跨いでも破棄されないように設定
        }
        else
        {
            Destroy(gameObject);  // すでにインスタンスがある場合は、このオブジェクトを破棄
        }
    }

    // 現在のシーン名を取得
    public string GetCurrentScene()
    {
        return currentSceneName;
    }

    // 現在のシーン名をセット
    public void SetCurrentScene(string sceneName)
    {
        currentSceneName = sceneName;
    }
}
