using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSizeController : MonoBehaviour
{
    public RectTransform leftArrow;  // �����
    public RectTransform rightArrow;  // �E���
    public Vector3 defaultScale = Vector3.one;  // �f�t�H���g�̃X�P�[��
    public Vector3 enlargedScale = new Vector3(1.2f, 1.2f, 1.2f);  // �傫���������̃X�P�[��

    // ���������ɖ��̃X�P�[����ݒ�
    void Start()
    {
        // �����̖��̃X�P�[����1�ɐݒ肷��
        ResetArrowScale();
    }

    // ���E�̖��̃T�C�Y��I����Ԃɉ����čX�V
    public void UpdateArrowSize(int selectedIndex)
    {
        // selectedIndex �� 0 �Ȃ獶����傫�����A�E���̓f�t�H���g�T�C�Y��
        if (selectedIndex == 0)
        {
            leftArrow.localScale = enlargedScale;   // ������傫��
            rightArrow.localScale = defaultScale;   // �E���͌��̃T�C�Y
        }
        // selectedIndex �� 1 �Ȃ�E����傫�����A�����̓f�t�H���g�T�C�Y��
        else if (selectedIndex == 1)
        {
            leftArrow.localScale = defaultScale;   // �����͌��̃T�C�Y
            rightArrow.localScale = enlargedScale; // �E����傫��
        }
    }

    // ���̃X�P�[�������Z�b�g���郁�\�b�h
    public void ResetArrowScale()
    {
        leftArrow.localScale = defaultScale;  // �����������T�C�Y��
        rightArrow.localScale = defaultScale; // �E���������T�C�Y��
    }
}
