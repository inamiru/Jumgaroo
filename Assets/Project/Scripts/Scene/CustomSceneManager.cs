using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // シーン管理を行うための名前空間
using TransitionsPlus;

namespace TransitionsPlusDemos
{
    public class CustomSceneManager : MonoBehaviour
    {
        private string lastPlayedStage;  // 最後にプレイしたステージ名を保存

        private static CustomSceneManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);  // シーンをまたいでもこのオブジェクトを保持
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // ステージ開始時に呼ばれる
        public void LoadStage(string stageName)
        {
            lastPlayedStage = stageName;  // 現在のステージ名を保存
            SceneManager.LoadScene(stageName);
        }

        // リスタート時に呼ばれる、保存していたステージを再ロード
        public void RestartLastPlayedStage()
        {
            if (!string.IsNullOrEmpty(lastPlayedStage))
            {
                SceneManager.LoadScene(lastPlayedStage);  // 保存していたステージをリスタート
            }
            else
            {
                Debug.LogWarning("No stage to restart. Last played stage is not set.");
            }
        }

        // ステージ選択画面に遷移
        public void LoadStageSelectScene()
        {
            SceneManager.LoadScene("StageSelectScene");
        }
    }
}
