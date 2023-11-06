using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using unityroom.Api;
using System;

public class GameOverLine : MonoBehaviour
{
    private float stayTime;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject ReStartButton;
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private Text txtScore;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("seed"))
        {
            //���Ԍv��
            stayTime += Time.deltaTime;
            //1�b��Ƀ��X�^�[�g����
            if (stayTime > 1.0f)
            {
                GameManager.RestartOnClickCount = 1;
                //GameOverText���\��
                GameOverText.SetActive(true);
                //GameOverCanvas���\��
                GameOverCanvas.SetActive(true);
                //GameOverCanvas���\��
                ReStartButton.SetActive(false);
                // C#�X�N���v�g�̖`���� `using unityroom.Api;` ��ǉ����Ă��������B
                // �{�[�hNo1�ɃX�R�A123.45f�𑗐M����B
                //1�ڂ̈����̓{�[�hNo�ł��Bunityroom�̐ݒ��ʂŃX�R�A�{�[�h�ꗗ��ʂŊm�F�ł��܂��B
                //2�ڂ̈����̓X�R�A(float)�ł��B
                //3�ڂ̈����̓X�R�A�̋L�^���[���łR��ނ���܂��Bunityroom�ō쐬�����X�R�A�{�[�h�Ɠ����ݒ��I��ł��������B
                //HighScoreDesc�F�n�C�X�R�A�i�~���j�Ƃ��ċL�^����
                //HighScoreAsc�F�n�C�X�R�A�i�����j�Ƃ��ċL�^����
                //Always�F�n�C�X�R�A�i��ɋL�^�j�Ƃ��ċL�^����
                UnityroomApiClient.Instance.SendScore(1, Convert.ToSingle(txtScore.text), ScoreboardWriteMode.Always);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("seed"))
        {
            //������
            stayTime = 0;
        }
    }
}
