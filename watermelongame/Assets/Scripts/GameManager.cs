using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isNext { get; set; }
    public int MaxSeedNo { get; private set; }

    seed seedIns;

    seed CreateseedIns;

    [SerializeField] private seed[] seedPrefab;
    [SerializeField] private Transform seedPosition;
    [SerializeField] private Text txtScore;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject ReStartButton;
    [SerializeField] private GameObject ReStartCanvas;
    [SerializeField] private GameObject GameOverText;
    [SerializeField] private GameObject GameOverCanvas;

    public static int RestartOnClickCount = 0;

    private int totalscore;

    int iCreateSeed;
    [SerializeField] private Transform NextPosition;
    Nextseed NextseedIns;
    public static int iNextCount;
    [SerializeField] private Nextseed[] NextseedPrefab;

    void Start()
    {
        Instance = this;
        isNext = false;
        //Prefabで設定した数がMAX数
        MaxSeedNo = seedPrefab.Length;
        totalscore = 0;
        SetScore(totalscore);
        //RestartButtonを非表示
        ReStartButton.SetActive(false);
        //RestartCanvasを非表示
        ReStartCanvas.SetActive(false);
        //GameOverTextを非表示
        GameOverText.SetActive(false);
        //GameOverCanvasを非表示
        GameOverCanvas.SetActive(false);
    }
    void Update()
    {
        //isNextがtrueの時、2秒後に物体を生成します。
        if (isNext)
        {
            isNext = false;
            //Invoke(string methodName, float time)
            //Invoke(“実行したい関数名”, 〇秒後) と記述することで、指定した関数を一定時間後に実行できます。
            //・第1引数には、関数名を文字列で指定する。
            //・第2引数には、何秒後に実行したいかをfloat型の値で指定する（整数でも可）。
            Invoke("DestoryNextSeed", 2f);
            Invoke("NextCreateSeed", 2.1f);
            Invoke("NextSeed", 2.1f);
        }
    }
    private void CreateSeed()
    {
        //一番大きなサイズから2つは出ない
        iCreateSeed = Random.Range(0, MaxSeedNo - 2);
        //int i = Random.Range(4, 4);
        //ゲーム中に表示される主人公や敵キャラクターなどのオブジェクトを生成する関数
        CreateseedIns = Instantiate(seedPrefab[iCreateSeed], seedPosition);
        //ランダムに選択された物を出現
        CreateseedIns.seedNo = iCreateSeed;
        //オブジェクトを表示する
        CreateseedIns.gameObject.SetActive(true);
    }
    private void NextCreateSeed()
    {
        //一番大きなサイズから2つは出ない
        iCreateSeed = NextseedIns.seedNo;
        //int i = Random.Range(4, 4);
        //ゲーム中に表示される主人公や敵キャラクターなどのオブジェクトを生成する関数
        CreateseedIns = Instantiate(seedPrefab[iCreateSeed], seedPosition);
        //ランダムに選択された物を出現
        CreateseedIns.seedNo = iCreateSeed;
        //オブジェクトを表示する
        CreateseedIns.gameObject.SetActive(true);
    }

    private void NextSeed()
    {
        //一番大きなサイズから2つは出ない
        iCreateSeed = Random.Range(0, MaxSeedNo - 2);
        //int i = Random.Range(4, 4);
        //ゲーム中に表示される主人公や敵キャラクターなどのオブジェクトを生成する関数
        NextseedIns = Instantiate(NextseedPrefab[iCreateSeed], NextPosition);
        //ランダムに選択された物を出現
        NextseedIns.seedNo = iCreateSeed;
        //オブジェクトを表示する
        NextseedIns.gameObject.SetActive(true);
    }

    private void DestoryNextSeed()
    {
        Destroy(NextseedIns.gameObject);
    }

    public void MergeNext(Vector3 target, int seedNo)
    {
        //Instantiate( 生成するオブジェクト,  場所(位置), 回転(向き) ,生成したオブジェクトの親要素を指定);  Quaternion.identity：回転はそのままなら
        seedIns = Instantiate(seedPrefab[seedNo + 1], target, Quaternion.identity, seedPosition);
        //下記でも実行可能
        //seed seedIns = Instantiate(seedPrefab[seedNo + 1], target, Quaternion.identity);
        //ひとつ大きいオブジェクトに変える
        seedIns.seedNo = seedNo + 1;
        seedIns.isDrop = true;
        //simulatedはRigidbody2Dの機能
        //Simulatedはオンにすると物理現象が適応され、オフにすると物理現象が適応されなくなります。
        //Rigidbody2Dを追加した段階ではオンになっているので物理現象が適応されます。
        //オフにすると物理現象が適応されなくなりますが、処理が軽くなります。
        seedIns.GetComponent<Rigidbody2D>().simulated = true;
        //オブジェクトを表示する
        seedIns.gameObject.SetActive(true);

        //Mathf.Pow：使えばべき乗（例：2の2乗、とか）を計算することができます。これは比較的処理が重い計算だとされています。
        //2乗
        //白→青：1点,青→黄：2点,黄→緑：4点,緑→赤：8点,赤→銅：16点,銅→銀：32点,銀→金：64点
        totalscore += (int)Mathf.Pow(2, seedNo);
        //3乗
        //白→青：1点,青→黄：3点,黄→緑：9点,緑→赤：27点,赤→銅：81点,銅→銀：243点,銀→金：729点
        //totalscore += (int)Mathf.Pow(3, seedNo);
        SetScore(totalscore);
    }
    private void SetScore(int score)
    {
        txtScore.text = score.ToString();
    }
    public void OnStartButton()
    {
        //StartButtonを非表示
        StartButton.SetActive(false);
        //RestartButtonを表示
        ReStartButton.SetActive(true);
        CreateSeed();
        NextSeed();
    }
    public void OnReStartButton()
    {
        //RestartCanvasを表示
        ReStartCanvas.SetActive(true);
        RestartOnClickCount = 1;
        //オブジェクトを非表示する
        //CreateseedIns.gameObject.SetActive(false);
        //オブジェクトを非表示する
        //seedIns.gameObject.SetActive(false);
    }
    public void OnYesButton()
    {
        RestartOnClickCount = 0;
        //最初からの画面に戻る
        SceneManager.LoadScene(0);
    }
    public void OnNoButton()
    {
        RestartOnClickCount = 0;
        //RestartCanvasを非表示
        ReStartCanvas.SetActive(false);
        //オブジェクトを非表示する
        //seedIns.gameObject.SetActive(true);
        //オブジェクトを非表示する
        //CreateseedIns.gameObject.SetActive(true);
    }
}
