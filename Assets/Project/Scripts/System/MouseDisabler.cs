using UnityEngine;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM  // InputSystemの場合
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

/// <summary>
/// マウス無効化機能。
/// マウスカーソルを非表示にし、クリックなども無効にします。
/// このスクリプトは、ゲームオブジェクトにアタッチしないでください。
/// Projectフォルダー配下に置いておくだけで、自動的に効果を発揮します。
/// </summary>
public class MouseDisabler : MonoBehaviour
{
    // ゲーム開始時に自動的に呼ばれ、
    // このコンポーネントがアタッチされたGameObjectを生成する
    [RuntimeInitializeOnLoadMethod]
    private static void RuntimeInitialize()
    {
        // 空のGameObjectを作成
        GameObject go = new GameObject("マウス無効化君");
        // 作ったGameObjectにこのクラスをアタッチ
        go.AddComponent<MouseDisabler>();
        // 全シーンにまたがって生存する（シーン切り替えで破棄されない）ようにする
        DontDestroyOnLoad(go);
    }

    void Update()
    {
#if !UNITY_EDITOR // エディターでは非表示にしない、exeでのみ非表示にする
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif

        // 現在有効な InputModule を取得
        EventSystem currentEventSystem = EventSystem.current;
        if (currentEventSystem == null) return;
        BaseInputModule currentInputModule = currentEventSystem.currentInputModule;
        if (currentInputModule == null) return;

#if ENABLE_INPUT_SYSTEM  // InputSystemの場合
        // 現在の InputModule が InputSystemUIInputModule である場合
        if (currentInputModule.GetType() == typeof(InputSystemUIInputModule))
        {
            InputSystemUIInputModule input = (InputSystemUIInputModule)currentInputModule;

            // マウス関連の処理を無効化する
            input.point = null;
            input.leftClick = null;
            input.middleClick = null;
            input.rightClick = null;
            input.scrollWheel = null;

            return;
        }
#endif

        // 現在の InputModule が StandaloneInputModule である場合
        if (currentInputModule.GetType() == typeof(StandaloneInputModule))
        {
            EnableCustomInputModule();
        }
    }

    // マウスを無効化したカスタム版StandaloneInputModule を有効化する
    void EnableCustomInputModule()
    {
        StandaloneInputModule standalone = (StandaloneInputModule)EventSystem.current.currentInputModule;

        // 元々ついていた StandaloneInputModole のプロパティを退避
        string horizontalAxis = standalone.horizontalAxis;
        string verticalAxis = standalone.verticalAxis;
        string submitButton = standalone.submitButton;
        string cancelButton = standalone.cancelButton;
        float inputActionsPerSecond = standalone.inputActionsPerSecond;
        float repeatDelay = standalone.repeatDelay;

        // 元々ついていた StandaloneInputModole は削除し、
        // マウスを無効化したカスタム版 InputModule をアタッチする。
        GameObject go = standalone.gameObject;
        Destroy(standalone);
        CustomInputModule custom = go.AddComponent<CustomInputModule>();

        // 退避しておいたプロパティを反映させる
        custom.horizontalAxis = horizontalAxis;
        custom.verticalAxis = verticalAxis;
        custom.submitButton = submitButton;
        custom.cancelButton = cancelButton;
        custom.inputActionsPerSecond = inputActionsPerSecond;
        custom.repeatDelay = repeatDelay;
    }

    // マウス・タッチ操作を無効化した StandaloneInputModule クラス
    class CustomInputModule : StandaloneInputModule
    {
        public override void Process()
        {
            bool usedEvent = SendUpdateEventToSelectedObject();

            if (eventSystem.sendNavigationEvents)
            {
                if (!usedEvent)
                    usedEvent |= SendMoveEventToSelectedObject();

                if (!usedEvent)
                    SendSubmitEventToSelectedObject();
            }
        }
    }
}