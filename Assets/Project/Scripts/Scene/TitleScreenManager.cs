using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TransitionsPlus;

namespace TransitionsPlusDemos {
    public class TitleScreenManager : MonoBehaviour
    {
        public TransitionAnimator animator;

        // Start is called before the first frame update
        void Update()
        {
            // スペースキーが押されたかどうかを検出
            if (Input.GetKeyDown(KeyCode.Return))
            {
                animator.Play();
            }
        }
    }
}
