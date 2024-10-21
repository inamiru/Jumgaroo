using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理を行うための名前空間
using TransitionsPlus;

namespace TransitionsPlusDemos
{
    public class CustomSceneManager : MonoBehaviour
    {
    // 現在のシーンをリロード（再読み込み）する
    public void RestartCurrentScene()
    {
        StartCoroutine(TransitionAndRestart());
    }

    // ステージ選択などの指定されたシーンに遷移する
    public void LoadScene(string sceneName)
    {
        StartCoroutine(TransitionAndLoadScene(sceneName));
    }

    // 指定されたシーンに遷移するコルーチン
    private IEnumerator TransitionAndLoadScene(string sceneName)
    {
        // シーン遷移を行う
        SceneManager.LoadScene(sceneName);

        // 遷移後に必要な処理があればここに追加
        yield return null; // コルーチンの終了
    }

    // 現在のシーンをリロードするコルーチン
    private IEnumerator TransitionAndRestart()
    {
        // 現在のシーンを再読み込み
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        // 再読み込み後に必要な処理があればここに追加
        yield return null; // コルーチンの終了
    }
}
}
