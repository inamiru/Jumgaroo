using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using TransitionsPlusDemos;

public class StageSelectManager : MonoBehaviour
{
    public Image thumbnailImage;  // �T���l�C����\������Image�R���|�[�l���g
    public TextMeshProUGUI stageNameText;  // �X�e�[�W����\������TextMeshPro
    public List<StageInfo> stages;  // �X�e�[�W���̃��X�g

    private int currentStageIndex = 0;  // ���ݑI������Ă���X�e�[�W�̃C���f�b�N�X

    private CustomSceneManager customSceneManager;

    // Start is called before the first frame update
    void Start()
    {
        // �ŏ��ɃX�e�[�W����\��
        UpdateStageDisplay();

        // CustomSceneManager�̃C���X�^���X���擾
        customSceneManager = FindObjectOfType<CustomSceneManager>();

        // ���݂̃V�[�������擾����CustomSceneManager�ɓn��
        if (customSceneManager != null)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;  // ���݂̃V�[�������擾
            customSceneManager.LoadStage(currentSceneName);  // �V�[������CustomSceneManager�ɓn���ĕۑ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �����L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousStage();
        }

        // �E���L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextStage();
        }

        // �X�y�[�X�L�[�őI�������X�e�[�W�����[�h
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadSelectedStage();
        }
    }

    // �X�e�[�W��1�O�ɐ؂�ւ���
    private void PreviousStage()
    {
        currentStageIndex--;
        if (currentStageIndex < 0)
        {
            currentStageIndex = stages.Count - 1;  // ���X�g�̍Ō�ɖ߂�
        }
        UpdateStageDisplay();
    }

    // �X�e�[�W��1��ɐ؂�ւ���
    private void NextStage()
    {
        currentStageIndex++;
        if (currentStageIndex >= stages.Count)
        {
            currentStageIndex = 0;  // ���X�g�̍ŏ��ɖ߂�
        }
        UpdateStageDisplay();
    }

    // �I������Ă���X�e�[�W��\������
    private void UpdateStageDisplay()
    {
        StageInfo currentStage = stages[currentStageIndex];
        thumbnailImage.sprite = currentStage.thumbnail;  // �T���l�C���摜��ύX
        stageNameText.text = currentStage.stageName;  // �X�e�[�W����ύX
    }

    // �I�������X�e�[�W�����[�h����
    private void LoadSelectedStage()
    {
        StageInfo selectedStage = stages[currentStageIndex];
        SceneManager.LoadScene(selectedStage.sceneName);  // �Ή�����V�[�������[�h
    }
}
