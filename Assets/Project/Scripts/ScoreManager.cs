using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // TextMeshPro���g�p���邽�߂ɕK�v


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;  // �V���O���g���C���X�^���X
    public int score = 0;                 // �X�R�A�̏����l
    public TextMeshProUGUI scoreText;     // �X�R�A��\������UI�e�L�X�g

    void Awake()
    {
        // �V���O���g���̎���
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // �X�R�A�Ǘ��I�u�W�F�N�g���V�[���J�ڂŔj�����Ȃ�
        }
        else
        {
            Destroy(gameObject);  // ���ɃC���X�^���X�����݂���ꍇ�́A�d�����Ȃ��悤�ɍ폜
        }
    }

    // �X�R�A�����Z���郁�\�b�h
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();  // UI���X�V
    }

    // UI��̃X�R�A�\�����X�V����
    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
