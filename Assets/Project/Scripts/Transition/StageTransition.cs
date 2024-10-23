using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;

    public class StageTransition : MonoBehaviour
    {
        public float delayTime = 3.0f;

        public TransitionAnimator stageClearAnimator;
        public TransitionAnimator gameOverAnimator;

        // 指定した時間（秒）後に処理を実行するコルーチン
        public IEnumerator CallAfterDelayStageClearTransition()
        {
            yield return new WaitForSeconds(delayTime);  // 指定した時間待つ

            // トランジション処理
            stageClearAnimator.Play();
        }

        // 指定した時間（秒）後に処理を実行するコルーチン
        public IEnumerator CallAfterDelayGameOVerTransition()
        {
            yield return new WaitForSeconds(delayTime);  // 指定した時間待つ

            // トランジション処理
            gameOverAnimator.Play();
        }
    }
