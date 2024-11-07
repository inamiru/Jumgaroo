using UnityEngine;

#if ENABLE_INPUT_SYSTEM  // InputSystem�̏ꍇ
using UnityEngine.InputSystem;
#endif

/// <summary>
/// �����I���@�\�B
/// ���̃X�N���v�g�́A�Q�[���I�u�W�F�N�g�ɃA�^�b�`���Ȃ��ł��������B
/// Project�t�H���_�[�z���ɒu���Ă��������ŁA�����I�Ɍ��ʂ𔭊����܂��B
/// </summary>
public class ForceQuit : MonoBehaviour
{
    // �Q�[���J�n���Ɏ����I�ɌĂ΂�A
    // ���̃R���|�[�l���g���A�^�b�`���ꂽGameObject�𐶐�����
    [RuntimeInitializeOnLoadMethod]
    private static void RuntimeInitialize()
    {
        // ���GameObject���쐬
        GameObject go = new GameObject("�����I���N");
        // �����GameObject�ɂ��̃N���X���A�^�b�`
        go.AddComponent<ForceQuit>();
        // �S�V�[���ɂ܂������Đ�������i�V�[���؂�ւ��Ŕj������Ȃ��j�悤�ɂ���
        DontDestroyOnLoad(go);
    }

    void Update()
    {
        // �����I���R�}���h�����͂��ꂽ��A�I������
        if (CheckQuitCommand())
        {
            Quit();
        }
    }

    // �����I���R�}���h�������ꂽ���𒲂ׂ�B
    // XBOX �R���g���[���[�� BACK �{�^���� START �{�^����������
    // �܂���
    // �L�[�{�[�h�̃G�X�P�[�v�L�[�������I���R�}���h�Ƃ���B
    bool CheckQuitCommand()
    {
#if ENABLE_LEGACY_INPUT_MANAGER // InputManager�̏ꍇ
        if ((Input.GetKey(KeyCode.JoystickButton6) && Input.GetKey(KeyCode.JoystickButton7))
            || Input.GetKey(KeyCode.Escape))
        {
            return true;
        }
#elif ENABLE_INPUT_SYSTEM // InputSystem�̏ꍇ

        Gamepad gamepad = Gamepad.current;

        if (gamepad != null && gamepad.selectButton.isPressed && gamepad.startButton.isPressed)
            return true;

        Keyboard keyboard = Keyboard.current;

        if (keyboard != null && keyboard.escapeKey.isPressed)
            return true;

#endif
        return false;
    }

    // �I������
    void Quit()
    {
#if UNITY_EDITOR // �G�f�B�^�[�̏ꍇ�́A�Đ��I������B
        UnityEditor.EditorApplication.isPlaying = false;
#else // �G�f�B�^�[�ȊO�iexe�Ȃǁj�̏ꍇ�́A�A�v���P�[�V�������I������B
        Application.Quit();
#endif
    }
}