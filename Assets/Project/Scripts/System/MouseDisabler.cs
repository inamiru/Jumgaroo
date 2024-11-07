using UnityEngine;
using UnityEngine.EventSystems;

#if ENABLE_INPUT_SYSTEM  // InputSystem�̏ꍇ
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

/// <summary>
/// �}�E�X�������@�\�B
/// �}�E�X�J�[�\�����\���ɂ��A�N���b�N�Ȃǂ������ɂ��܂��B
/// ���̃X�N���v�g�́A�Q�[���I�u�W�F�N�g�ɃA�^�b�`���Ȃ��ł��������B
/// Project�t�H���_�[�z���ɒu���Ă��������ŁA�����I�Ɍ��ʂ𔭊����܂��B
/// </summary>
public class MouseDisabler : MonoBehaviour
{
    // �Q�[���J�n���Ɏ����I�ɌĂ΂�A
    // ���̃R���|�[�l���g���A�^�b�`���ꂽGameObject�𐶐�����
    [RuntimeInitializeOnLoadMethod]
    private static void RuntimeInitialize()
    {
        // ���GameObject���쐬
        GameObject go = new GameObject("�}�E�X�������N");
        // �����GameObject�ɂ��̃N���X���A�^�b�`
        go.AddComponent<MouseDisabler>();
        // �S�V�[���ɂ܂������Đ�������i�V�[���؂�ւ��Ŕj������Ȃ��j�悤�ɂ���
        DontDestroyOnLoad(go);
    }

    void Update()
    {
#if !UNITY_EDITOR // �G�f�B�^�[�ł͔�\���ɂ��Ȃ��Aexe�ł̂ݔ�\���ɂ���
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif

        // ���ݗL���� InputModule ���擾
        EventSystem currentEventSystem = EventSystem.current;
        if (currentEventSystem == null) return;
        BaseInputModule currentInputModule = currentEventSystem.currentInputModule;
        if (currentInputModule == null) return;

#if ENABLE_INPUT_SYSTEM  // InputSystem�̏ꍇ
        // ���݂� InputModule �� InputSystemUIInputModule �ł���ꍇ
        if (currentInputModule.GetType() == typeof(InputSystemUIInputModule))
        {
            InputSystemUIInputModule input = (InputSystemUIInputModule)currentInputModule;

            // �}�E�X�֘A�̏����𖳌�������
            input.point = null;
            input.leftClick = null;
            input.middleClick = null;
            input.rightClick = null;
            input.scrollWheel = null;

            return;
        }
#endif

        // ���݂� InputModule �� StandaloneInputModule �ł���ꍇ
        if (currentInputModule.GetType() == typeof(StandaloneInputModule))
        {
            EnableCustomInputModule();
        }
    }

    // �}�E�X�𖳌��������J�X�^����StandaloneInputModule ��L��������
    void EnableCustomInputModule()
    {
        StandaloneInputModule standalone = (StandaloneInputModule)EventSystem.current.currentInputModule;

        // ���X���Ă��� StandaloneInputModole �̃v���p�e�B��ޔ�
        string horizontalAxis = standalone.horizontalAxis;
        string verticalAxis = standalone.verticalAxis;
        string submitButton = standalone.submitButton;
        string cancelButton = standalone.cancelButton;
        float inputActionsPerSecond = standalone.inputActionsPerSecond;
        float repeatDelay = standalone.repeatDelay;

        // ���X���Ă��� StandaloneInputModole �͍폜���A
        // �}�E�X�𖳌��������J�X�^���� InputModule ���A�^�b�`����B
        GameObject go = standalone.gameObject;
        Destroy(standalone);
        CustomInputModule custom = go.AddComponent<CustomInputModule>();

        // �ޔ����Ă������v���p�e�B�𔽉f������
        custom.horizontalAxis = horizontalAxis;
        custom.verticalAxis = verticalAxis;
        custom.submitButton = submitButton;
        custom.cancelButton = cancelButton;
        custom.inputActionsPerSecond = inputActionsPerSecond;
        custom.repeatDelay = repeatDelay;
    }

    // �}�E�X�E�^�b�`����𖳌������� StandaloneInputModule �N���X
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