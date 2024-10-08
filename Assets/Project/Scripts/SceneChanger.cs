using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーンを管理するための名前空間
using UnityEngine.UI;  // UIボタンの名前空間
using TMPro;  // TextMeshPro用の名前空間

public class Scenechanger : MonoBehaviour
{
    public Button switchSceneButton;  // ボタンの参照
    public string sceneName;  // 切り替えるシーンの名前

    // Start is called before the first frame update
    void Start()
    {
        // ボタンにリスナーを追加して、押されたときにシーンを切り替える
        switchSceneButton.onClick.AddListener(OnSwitchScene);

    }

    // ボタンが押された時に呼び出されるメソッド
    void OnSwitchScene()
    {
        // シーンをロードする
        SceneManager.LoadScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
