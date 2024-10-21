using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransitionsPlus;

namespace TransitionsPlusDemos
{
    public class ResultScreenManager : MonoBehaviour
    {
        // �؂�ւ������V�[���̖��O��Inspector����ݒ�\
        public string nextSceneName = "GameScene";
        public TransitionAnimator animator;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // �X�y�[�X�L�[�������ꂽ���ǂ��������o
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.Play();
            }
        }
    }
}
