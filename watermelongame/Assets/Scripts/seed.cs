using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;

//スクリプトのポイント
//マウスを動かしたら物体の位置を変更。Mathf.Clampは横の最大値で枠外に行かないようにしています。
//マウスクリック後はマウス操作で物体を動かさず、落下させます。
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
        //左クリック時
        //if (Input.GetMouseButton(0) && isDrop == false)

        if (TapFlag == true && isDrop == false)
        {
            if (GameManager.RestartOnClickCount == 1)
            {
                //何もしない
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

        //座標位置を決定している
        Move();
    }
    private void Drop()
    {
        //TapFlag = false;

        //
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
    private void Move()
    {
        if (GameManager.RestartOnClickCount == 1)
        {
            //スクリーン座標をワールド座標に変換
            //ワールド座標：ゲームのキャラクターが、存在している空間。
            //スクリーン座標：カメラが表示している範囲。
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.Clamp(mousePos.x, 0f, 0f);
            mousePos.y = 3.5f;
            transform.position = mousePos;
        }
        else if (GameManager.RestartOnClickCount == 0)
        {
            //スクリーン座標をワールド座標に変換
            //ワールド座標：ゲームのキャラクターが、存在している空間。
            //スクリーン座標：カメラが表示している範囲。
            //Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.Clamp(mousePos.x, -1.5f, 1.5f);
            mousePos.y = 3.5f;
            transform.position = mousePos;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //衝突された方のオブジェクトの名前を取得
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
    //オブジェクトが押された(タップされた)イベント発生時に呼び出される関数
    public void OnPointerDown()
    {
        Debug.Log("押したよ");

        //押された状態に設定
        TapFlag = true;
    }
    //オブジェクトから指が離れた(タップをやめた)イベント発生時に呼び出される関数
    public void OnPointerUp()
    {
        //押されていない状態に設定
        TapFlag = false;
    }
}
