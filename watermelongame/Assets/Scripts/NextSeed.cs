using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nextseed : MonoBehaviour
{
    private Rigidbody2D _rb;
    //public bool isMergeFlag = false;
    public bool isDrop = false;
    public int seedNo;
    Vector2 mousePos;

    public bool TapFlag;
    public GameObject gameManager;

    void Start()
    {
        
    }
    void Update()
    {
        if (isDrop)
        {
            return;
        }
 
        //���W�ʒu�����肵�Ă���
        Move();
    }
    /*
    private void Drop()
    {
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
    */
    private void Move()
    {
        //�X�N���[�����W�����[���h���W�ɕϊ�
        //���[���h���W�F�Q�[���̃L�����N�^�[���A���݂��Ă����ԁB
        //�X�N���[�����W�F�J�������\�����Ă���͈́B
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = 5.0f;
        mousePos.y = 3.0f;
        transform.position = mousePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Nextseed Nextcolseed = collision.gameObject.GetComponent<Nextseed>();
        //Destroy(Nextcolseed.gameObject);

        //�Փ˂��ꂽ���̃I�u�W�F�N�g�̖��O���擾
        //GameObject colobj = collision.gameObject;

        //Nextseed colseed = collision.gameObject.GetComponent<Nextseed>();

        //isMergeFlag = true;
        //colseed.isMergeFlag = true;
        //GameManager.Instance.MergeNext(transform.position, seedNo);

        //Destroy(gameObject);
        //Destroy(colseed.gameObject);


        /*
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
        */
    }
}
