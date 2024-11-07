using UnityEngine;

#if ENABLE_INPUT_SYSTEM  // InputSystemの場合
using UnityEngine.InputSystem;
#endif

/// <summary>
/// 強制終了機能。
/// このスクリプトは、ゲームオブジェクトにアタッチしないでください。
/// Projectフォルダー配下に置いておくだけで、自動的に効果を発揮します。
/// </summary>
public class ForceQuit : MonoBehaviour
{
    // ゲーム開始時に自動的に呼ばれ、
    // このコンポーネントがアタッチされたGameObjectを生成する
    [RuntimeInitializeOnLoadMethod]
    private static void RuntimeInitialize()
    {
        // 空のGameObjectを作成
        GameObject go = new GameObject("強制終了君");
        // 作ったGameObjectにこのクラスをアタッチ
        go.AddComponent<ForceQuit>();
        // 全シーンにまたがって生存する（シーン切り替えで破棄されない）ようにする
        DontDestroyOnLoad(go);
    }

    void Update()
    {
        // 強制終了コマンドが入力されたら、終了する
        if (CheckQuitCommand())
        {
            Quit();
        }
    }

    // 強制終了コマンドが押されたかを調べる。
    // XBOX コントローラーの BACK ボタンと START ボタン同時押し
    // または
    // キーボードのエスケープキーを強制終了コマンドとする。
    bool CheckQuitCommand()
    {
#if ENABLE_LEGACY_INPUT_MANAGER // InputManagerの場合
        if ((Input.GetKey(KeyCode.JoystickButton6) && Input.GetKey(KeyCode.JoystickButton7))
            || Input.GetKey(KeyCode.Escape))
        {
            return true;
        }
#elif ENABLE_INPUT_SYSTEM // InputSystemの場合

        Gamepad gamepad = Gamepad.current;

        if (gamepad != null && gamepad.selectButton.isPressed && gamepad.startButton.isPressed)
            return true;

        Keyboard keyboard = Keyboard.current;

        if (keyboard != null && keyboard.escapeKey.isPressed)
            return true;

#endif
        return false;
    }

    // 終了処理
    void Quit()
    {
#if UNITY_EDITOR // エディターの場合は、再生終了する。
        UnityEditor.EditorApplication.isPlaying = false;
#else // エディター以外（exeなど）の場合は、アプリケーションを終了する。
        Application.Quit();
#endif
    }
}