using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject[] effects; // �����̃G�t�F�N�g�̃v���n�u���i�[����z��
    public Transform[] effectPositions; // �G�t�F�N�g��\������ʒu�̔z��

    public float delayTime = 3.0f;  // �G�t�F�N�g���Đ�����܂ł̒x������

    void Start()
    {
        // �w�肵�����Ԍ�ɃG�t�F�N�g���Đ�����R���[�`�����J�n
        StartCoroutine(SpawnEffectsAfterDelay());
    }

    public IEnumerator SpawnEffectsAfterDelay()
    {
        // �w�肵���b���ҋ@
        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < effects.Length; i++)
        {
            if (i < effectPositions.Length)
            {
                // �G�t�F�N�g�𐶐�������ɁA���̈ʒu���Œ肷��
                GameObject effect = Instantiate(effects[i], effectPositions[i].position, effectPositions[i].rotation);
                effect.transform.position = effectPositions[i].position; // �ʒu���Œ�

            }
            else
            {
                // �ʒu������Ȃ��ꍇ�̓S�[���G���A�̃��[���h���W�ɐ���
                Instantiate(effects[i], transform.position, Quaternion.identity);
            }
        }

    }
}
