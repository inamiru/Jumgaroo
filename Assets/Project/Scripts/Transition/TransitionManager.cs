using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;
using UnityEngine.SceneManagement;


public class TransitionManager : MonoBehaviour
{
    public float duration = 2.0f;                                 // �g�����W�V�����̎���
    public int rotationMultiplier = -2;                           // ��]�̃}���`�v���C���[
    public int splits = 2;                                        // ������
    public bool keepAspectRatio = true;                           // �A�X�y�N�g���ێ����邩

    // �g�����W�V�����̕��@��I������t���O
    public bool useGradient = false;                              // �O���f�[�V�������g�p���邩
    public string colorStartHex = "#F8732B";                      // �O���f�[�V�����J�n�F
    public string colorEndHex = "#D6B436";                        // �O���f�[�V�����I���F
    public string solidColorHex = "#000000";                      // �P�F�g�����W�V�����̐F


    // �O���f�[�V�����ɂ��g�����W�V����
    public void StartGradientTransition(string sceneNameToLoad)
    {
        Gradient gradient = new Gradient();

        // 16�i���J���[�R�[�h����Color�𐶐�
        Color colorStart;
        Color colorEnd;

        ColorUtility.TryParseHtmlString("#F8732B", out colorStart);
        ColorUtility.TryParseHtmlString("#D6B436", out colorEnd);

        // GradientColorKey�z����쐬���AColor�Ǝ��Ԃ��w��
        gradient.colorKeys = new GradientColorKey[] {
            new GradientColorKey(colorStart, 0.0f),  // �ŏ��̐F
            new GradientColorKey(colorEnd, 1.0f)     // �Ō�̐F
        };

        // �g�����W�V�������J�n
        TransitionAnimator.Start(
            TransitionType.Shape,     // transition type
            duration: duration,            // transition duration in seconds
            rotationMultiplier: rotationMultiplier,
            splits: splits,
            gradient: gradient,
            keepAspectRatio: keepAspectRatio
            );
    }

    // �P��F�ɂ��g�����W�V����
    public void StartColorTransition(Color transitionColor)
    {
        // �g�����W�V�������J�n
        TransitionAnimator.Start(
            TransitionType.Shape,     // transition type
            duration: duration,
            rotationMultiplier: rotationMultiplier,
            splits: splits,
            color: transitionColor,
            keepAspectRatio: keepAspectRatio
        );
    }

    // �ǂ��炩�̃g�����W�V�������Ăяo�����\�b�h
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

        // �g�����W�V����������������ɃV�[�������[�h����
        if (sceneNameToLoad != null)
        {
            StartCoroutine(LoadSceneAfterTransition(sceneNameToLoad));
        }
    }

        private IEnumerator LoadSceneAfterTransition(string sceneName)
        {
            // �g�����W�V��������������܂ő҂i���ۂ̎����ɉ����ď�����ǉ��j
            yield return new WaitForSeconds(2.0f); // ���̒x���i2�b�ҋ@�j

            SceneManager.LoadScene(sceneName); // �V�[�������[�h
        }
}
