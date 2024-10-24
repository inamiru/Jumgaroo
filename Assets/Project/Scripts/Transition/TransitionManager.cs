using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;
using UnityEngine.SceneManagement;


public class TransitionManager : MonoBehaviour
{
    public float duration = 2.0f;                                 // トランジションの時間
    public int rotationMultiplier = -2;                           // 回転のマルチプライヤー
    public int splits = 2;                                        // 分割数
    public bool keepAspectRatio = true;                           // アスペクト比を保持するか

    // トランジションの方法を選択するフラグ
    public bool useGradient = false;                              // グラデーションを使用するか
    public string colorStartHex = "#F8732B";                      // グラデーション開始色
    public string colorEndHex = "#D6B436";                        // グラデーション終了色
    public string solidColorHex = "#000000";                      // 単色トランジションの色


    // グラデーションによるトランジション
    public void StartGradientTransition(string sceneNameToLoad)
    {
        Gradient gradient = new Gradient();

        // 16進数カラーコードからColorを生成
        Color colorStart;
        Color colorEnd;

        ColorUtility.TryParseHtmlString("#F8732B", out colorStart);
        ColorUtility.TryParseHtmlString("#D6B436", out colorEnd);

        // GradientColorKey配列を作成し、Colorと時間を指定
        gradient.colorKeys = new GradientColorKey[] {
            new GradientColorKey(colorStart, 0.0f),  // 最初の色
            new GradientColorKey(colorEnd, 1.0f)     // 最後の色
        };

        // トランジションを開始
        TransitionAnimator.Start(
            TransitionType.Shape,     // transition type
            duration: duration,            // transition duration in seconds
            rotationMultiplier: rotationMultiplier,
            splits: splits,
            gradient: gradient,
            keepAspectRatio: keepAspectRatio
            );
    }

    // 単一色によるトランジション
    public void StartColorTransition(Color transitionColor)
    {
        // トランジションを開始
        TransitionAnimator.Start(
            TransitionType.Shape,     // transition type
            duration: duration,
            rotationMultiplier: rotationMultiplier,
            splits: splits,
            color: transitionColor,
            keepAspectRatio: keepAspectRatio
        );
    }

    // どちらかのトランジションを呼び出すメソッド
    public void ExecuteTransition(bool useGradient, string sceneNameToLoad = null, Color? transitionColor = null)
    {
        if (useGradient)
        {
            StartGradientTransition(sceneNameToLoad);
        }
        else if (transitionColor.HasValue)
        {
            StartColorTransition(transitionColor.Value);
        }
        else
        {
            Debug.LogWarning("No transition color specified for color transition.");
        }

        // トランジションが完了した後にシーンをロードする
        if (sceneNameToLoad != null)
        {
            StartCoroutine(LoadSceneAfterTransition(sceneNameToLoad));
        }
    }

        private IEnumerator LoadSceneAfterTransition(string sceneName)
        {
            // トランジションが完了するまで待つ（実際の実装に応じて条件を追加）
            yield return new WaitForSeconds(2.0f); // 仮の遅延（2秒待機）

            SceneManager.LoadScene(sceneName); // シーンをロード
        }
}
