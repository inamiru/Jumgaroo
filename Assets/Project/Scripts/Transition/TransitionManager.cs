using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;  // トランジション用のライブラリを使用
using UnityEngine.SceneManagement;  // シーン管理を行うためのライブラリを使用

public class TransitionManager : MonoBehaviour
{
    public float duration = 2.0f;  // トランジションの継続時間
    public int rotationMultiplier = -2;  // トランジション時の回転倍率
    public int splits = 2;  // トランジションエフェクトで分割する数
    public bool keepAspectRatio = true;  // アスペクト比を維持するかどうか

    // トランジションエフェクトの見た目を変更するオプション
    public bool useGradient = false;  // グラデーションを使用するかどうか
    public string colorStartHex = "#F8732B";  // グラデーションの開始色（16進数）
    public string colorEndHex = "#D6B436";  // グラデーションの終了色（16進数）
    public string solidColorHex = "#000000";  // 単色のトランジションの場合の色

    // グラデーショントランジションを開始するメソッド
    public void StartGradientTransition(string sceneNameToLoad)
    {
        Gradient gradient = new Gradient();  // グラデーションオブジェクトを作成

        // 16進数のカラーコードをColor型に変換
        Color colorStart;
        Color colorEnd;

        // 開始色と終了色を16進数コードから変換
        ColorUtility.TryParseHtmlString(colorStartHex, out colorStart);
        ColorUtility.TryParseHtmlString(colorEndHex, out colorEnd);

        // グラデーションの色設定を行う（開始色と終了色）
        gradient.colorKeys = new GradientColorKey[] {
            new GradientColorKey(colorStart, 0.0f),  // 開始時の色
            new GradientColorKey(colorEnd, 1.0f)     // 終了時の色
        };

        // トランジションアニメーションを開始する
        TransitionAnimator.Start(
            TransitionType.Shape,  // トランジションのタイプ（ここではShape）
            duration: duration,  // トランジションの継続時間
            rotationMultiplier: rotationMultiplier,  // 回転倍率
            splits: splits,  // 分割数
            gradient: gradient,  // グラデーションを適用
            keepAspectRatio: keepAspectRatio  // アスペクト比を維持するかどうか
        );
    }

    // 単色のトランジションを開始するメソッド
    public void StartColorTransition(Color transitionColor)
    {
        // 単色のトランジションを開始
        TransitionAnimator.Start(
            TransitionType.Shape,  // トランジションのタイプ
            duration: duration,  // トランジションの継続時間
            rotationMultiplier: rotationMultiplier,  // 回転倍率
            splits: splits,  // 分割数
            color: transitionColor,  // 単色のトランジションに使用する色
            keepAspectRatio: keepAspectRatio  // アスペクト比を維持するかどうか
        );
    }

    // トランジションを実行するためのメソッド（グラデーションか単色かを選ぶ）
    public void ExecuteTransition(bool useGradient, string sceneNameToLoad = null, Color? transitionColor = null)
    {
        if (useGradient)  // グラデーションが有効ならグラデーショントランジションを開始
        {
            StartGradientTransition(sceneNameToLoad);
        }
        else if (transitionColor.HasValue)  // 単色が指定されているなら単色トランジションを開始
        {
            StartColorTransition(transitionColor.Value);
        }
        else  // トランジションカラーが指定されていない場合は警告を出す
        {
            Debug.LogWarning("No transition color specified for color transition.");
        }

        // トランジション完了後にシーンを読み込む
        if (sceneNameToLoad != null)
        {
            StartCoroutine(LoadSceneAfterTransition(sceneNameToLoad));  // コルーチンでシーンを遅延ロード
        }
    }

    // 指定されたシーンをトランジション後にロードするコルーチン
    private IEnumerator LoadSceneAfterTransition(string sceneName)
    {
        // トランジションの終了まで待機
        yield return new WaitForSeconds(duration);  // duration秒待つ

        SceneManager.LoadScene(sceneName);  // 指定されたシーンをロード
    }
}
