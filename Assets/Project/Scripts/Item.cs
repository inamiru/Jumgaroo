using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int scoreValue = 10;  // ���̃A�C�e�����l�����ꂽ�ۂɉ��Z����X�R�A

    private void OnTriggerEnter(Collider other)
    {
        // �v���C���[�ɐڐG�����ꍇ
        if (other.CompareTag("Player"))
        {
            // �X�R�A�����Z
            ScoreManager.instance.AddScore(scoreValue);

            // �A�C�e�����폜
            Destroy(gameObject);
        }
    }
}
