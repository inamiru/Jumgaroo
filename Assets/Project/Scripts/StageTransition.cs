using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;

namespace TransitionsPlusDemos
{
    public class StageTransition : MonoBehaviour
    {
        public float delayTime = 3.0f;

        public TransitionAnimator animator;

        // 指定した時間（秒）後に処理を実行するコルーチン
        public IEnumerator CallAfterDelayTransition()
        {
            yield return new WaitForSeconds(delayTime);  // 指定した時間待つ
        
            // トランジション処理
            animator.Play();
        }
    }
}
