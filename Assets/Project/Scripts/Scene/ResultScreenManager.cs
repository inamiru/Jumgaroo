using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;

namespace TransitionsPlusDemos
{
    public class ResultScreenManager : MonoBehaviour
    {
        // 切り替えたいシーンの名前をInspectorから設定可能
        public string nextSceneName = "GameScene";
        public TransitionAnimator animator;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // スペースキーが押されたかどうかを検出
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.Play();
            }
        }
    }
}
