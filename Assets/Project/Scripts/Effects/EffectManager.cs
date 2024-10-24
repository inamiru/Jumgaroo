using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;  // �V���O���g���C���X�^���X

    public GameObject dustEffectPrefab;    // �������̓y���G�t�F�N�g�̃v���t�@�u
    public GameObject heartLostEffectPrefab; // �n�[�g���������Ƃ��̃G�t�F�N�g�̃v���t�@�u

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // �V�[���ԂŃI�u�W�F�N�g���j������Ȃ��悤�ɂ���
        }
        else
        {
            Destroy(gameObject);  // ���̃C���X�^���X�����݂���ꍇ�́A�V�����I�u�W�F�N�g��j��
        }
    }

    // �y���G�t�F�N�g���Đ����郁�\�b�h
    public void PlayDustEffect(Vector3 position)
    {
        Instantiate(dustEffectPrefab, position, Quaternion.identity);
    }

    // �n�[�g���������Ƃ��̃G�t�F�N�g���Đ����郁�\�b�h
    public void PlayHeartLostEffect(Vector3 position)
    {
        Instantiate(heartLostEffectPrefab, position, Quaternion.identity);
    }
}
