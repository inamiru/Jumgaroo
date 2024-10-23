using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理を行うための名前空間

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager instance;

    private string previousSceneName = "TitleScene";  // タイトル画面を初期値に設定
    
    // ステージ選択シーンの名前を定数で管理
    private const string stageSelectSceneName = "StageSelectScene";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // オブジェクトがシーン間で破棄されないようにする
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // 別のインスタンスが存在する場合は、新しいオブジェクトを破棄
        }
    }

    public void LoadScene(string sceneName)
    {
        // シーン遷移前に現在のシーン名を保存
        StageController.instance.SetCurrentScene(SceneManager.GetActiveScene().name);

        // シーンの読み込み
        SceneManager.LoadScene(sceneName);
    }



    public string GetPreviousSceneName()
    {
        return previousSceneName;
    }

    // ステージ選択画面をロード
    public void LoadStageSelectScene()
    {
        SceneManager.LoadScene(stageSelectSceneName);   // 指定されたシーンをロード
    }
}
