using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageStart : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // 現在のシーン名を StageController に設定
        StageController.instance.SetCurrentScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
