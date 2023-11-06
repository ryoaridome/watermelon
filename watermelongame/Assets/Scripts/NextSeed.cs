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
 
        //座標位置を決定している
        Move();
    }
    /*
    private void Drop()
    {
        isDrop = true;

        //simulatedはRigidbody2Dの機能
        //Simulatedはオンにすると物理現象が適応され、オフにすると物理現象が適応されなくなります。
        //Rigidbody2Dを追加した段階ではオンになっているので物理現象が適応されます。
        //オフにすると物理現象が適応されなくなりますが、処理が軽くなります。
        _rb.simulated = true;

        //trueに修正
        //GameManagerの条件文によりPrefabを作成する
        GameManager.Instance.isNext = true;
    }
    */
    private void Move()
    {
        //スクリーン座標をワールド座標に変換
        //ワールド座標：ゲームのキャラクターが、存在している空間。
        //スクリーン座標：カメラが表示している範囲。
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = 5.0f;
        mousePos.y = 3.0f;
        transform.position = mousePos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Nextseed Nextcolseed = collision.gameObject.GetComponent<Nextseed>();
        //Destroy(Nextcolseed.gameObject);

        //衝突された方のオブジェクトの名前を取得
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
