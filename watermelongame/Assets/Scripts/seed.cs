using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

//�X�N���v�g�̃|�C���g
//�}�E�X�𓮂������畨�̂̈ʒu��ύX�BMathf.Clamp�͉��̍ő�l�Řg�O�ɍs���Ȃ��悤�ɂ��Ă��܂��B
//�}�E�X�N���b�N��̓}�E�X����ŕ��̂𓮂������A���������܂��B
public class seed : MonoBehaviour
{
    private Rigidbody2D _rb;
    public bool isMergeFlag = false;
    public bool isDrop = false;
    public int seedNo;
    Vector2 mousePos;

    public bool TapFlag;
    public GameObject gameManager;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //���N���b�N��
        //if (Input.GetMouseButton(0) && isDrop == false)

        if (TapFlag == true && isDrop == false)
        {
            if (GameManager.RestartOnClickCount == 1)
            {
                //�������Ȃ�
            }
            else if (GameManager.RestartOnClickCount == 0)
            {
                Drop();
            }
        }

        if (isDrop)
        {
            return;
        }

        //���W�ʒu�����肵�Ă���
        Move();
    }
    private void Drop()
    {
        //TapFlag = false;

        //
        isDrop = true;

        //simulated��Rigidbody2D�̋@�\
        //Simulated�̓I���ɂ���ƕ������ۂ��K������A�I�t�ɂ���ƕ������ۂ��K������Ȃ��Ȃ�܂��B
        //Rigidbody2D��ǉ������i�K�ł̓I���ɂȂ��Ă���̂ŕ������ۂ��K������܂��B
        //�I�t�ɂ���ƕ������ۂ��K������Ȃ��Ȃ�܂����A�������y���Ȃ�܂��B
        _rb.simulated = true;

        //true�ɏC��
        //GameManager�̏������ɂ��Prefab���쐬����
        GameManager.Instance.isNext = true;
    }
    private void Move()
    {
        if (GameManager.RestartOnClickCount == 1)
        {
            //�X�N���[�����W�����[���h���W�ɕϊ�
            //���[���h���W�F�Q�[���̃L�����N�^�[���A���݂��Ă����ԁB
            //�X�N���[�����W�F�J�������\�����Ă���͈́B
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.Clamp(mousePos.x, 0f, 0f);
            mousePos.y = 3.5f;
            transform.position = mousePos;
        }
        else if (GameManager.RestartOnClickCount == 0)
        {
            //�X�N���[�����W�����[���h���W�ɕϊ�
            //���[���h���W�F�Q�[���̃L�����N�^�[���A���݂��Ă����ԁB
            //�X�N���[�����W�F�J�������\�����Ă���͈́B
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.Clamp(mousePos.x, -1.5f, 1.5f);
            mousePos.y = 3.5f;
            transform.position = mousePos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�Փ˂��ꂽ���̃I�u�W�F�N�g�̖��O���擾
        GameObject colobj = collision.gameObject;
        if (colobj.CompareTag("seed"))
        {
            seed colseed = collision.gameObject.GetComponent<seed>();
            if (seedNo == colseed.seedNo &&
                !isMergeFlag &&
                !colseed.isMergeFlag &&
                seedNo < GameManager.Instance.MaxSeedNo - 1)
            {
                isMergeFlag = true;
                colseed.isMergeFlag = true;
                GameManager.Instance.MergeNext(transform.position, seedNo);
                Destroy(gameObject);
                Destroy(colseed.gameObject);
            }
        }
    }
    //�I�u�W�F�N�g�������ꂽ(�^�b�v���ꂽ)�C�x���g�������ɌĂяo�����֐�
    public void OnPointerDown()
    {
        Debug.Log("��������");

        //�����ꂽ��Ԃɐݒ�
        TapFlag = true;
    }
    //�I�u�W�F�N�g����w�����ꂽ(�^�b�v����߂�)�C�x���g�������ɌĂяo�����֐�
    public void OnPointerUp()
    {
        //������Ă��Ȃ���Ԃɐݒ�
        TapFlag = false;
    }
}
