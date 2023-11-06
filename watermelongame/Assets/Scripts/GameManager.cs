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
        //Prefab�Őݒ肵������MAX��
        MaxSeedNo = seedPrefab.Length;
        totalscore = 0;
        SetScore(totalscore);
        //RestartButton���\��
        ReStartButton.SetActive(false);
        //RestartCanvas���\��
        ReStartCanvas.SetActive(false);
        //GameOverText���\��
        GameOverText.SetActive(false);
        //GameOverCanvas���\��
        GameOverCanvas.SetActive(false);
    }
    void Update()
    {
        //isNext��true�̎��A2�b��ɕ��̂𐶐����܂��B
        if (isNext)
        {
            isNext = false;
            //Invoke(string methodName, float time)
            //Invoke(�g���s�������֐����h, �Z�b��) �ƋL�q���邱�ƂŁA�w�肵���֐�����莞�Ԍ�Ɏ��s�ł��܂��B
            //�E��1�����ɂ́A�֐����𕶎���Ŏw�肷��B
            //�E��2�����ɂ́A���b��Ɏ��s����������float�^�̒l�Ŏw�肷��i�����ł��j�B
            Invoke("DestoryNextSeed", 2f);
            Invoke("NextCreateSeed", 2.1f);
            Invoke("NextSeed", 2.1f);
        }
    }
    private void CreateSeed()
    {
        //��ԑ傫�ȃT�C�Y����2�͏o�Ȃ�
        iCreateSeed = Random.Range(0, MaxSeedNo - 2);
        //int i = Random.Range(4, 4);
        //�Q�[�����ɕ\��������l����G�L�����N�^�[�Ȃǂ̃I�u�W�F�N�g�𐶐�����֐�
        CreateseedIns = Instantiate(seedPrefab[iCreateSeed], seedPosition);
        //�����_���ɑI�����ꂽ�����o��
        CreateseedIns.seedNo = iCreateSeed;
        //�I�u�W�F�N�g��\������
        CreateseedIns.gameObject.SetActive(true);
    }
    private void NextCreateSeed()
    {
        //��ԑ傫�ȃT�C�Y����2�͏o�Ȃ�
        iCreateSeed = NextseedIns.seedNo;
        //int i = Random.Range(4, 4);
        //�Q�[�����ɕ\��������l����G�L�����N�^�[�Ȃǂ̃I�u�W�F�N�g�𐶐�����֐�
        CreateseedIns = Instantiate(seedPrefab[iCreateSeed], seedPosition);
        //�����_���ɑI�����ꂽ�����o��
        CreateseedIns.seedNo = iCreateSeed;
        //�I�u�W�F�N�g��\������
        CreateseedIns.gameObject.SetActive(true);
    }

    private void NextSeed()
    {
        //��ԑ傫�ȃT�C�Y����2�͏o�Ȃ�
        iCreateSeed = Random.Range(0, MaxSeedNo - 2);
        //int i = Random.Range(4, 4);
        //�Q�[�����ɕ\��������l����G�L�����N�^�[�Ȃǂ̃I�u�W�F�N�g�𐶐�����֐�
        NextseedIns = Instantiate(NextseedPrefab[iCreateSeed], NextPosition);
        //�����_���ɑI�����ꂽ�����o��
        NextseedIns.seedNo = iCreateSeed;
        //�I�u�W�F�N�g��\������
        NextseedIns.gameObject.SetActive(true);
    }

    private void DestoryNextSeed()
    {
        Destroy(NextseedIns.gameObject);
    }

    public void MergeNext(Vector3 target, int seedNo)
    {
        //Instantiate( ��������I�u�W�F�N�g,  �ꏊ(�ʒu), ��](����) ,���������I�u�W�F�N�g�̐e�v�f���w��);  Quaternion.identity�F��]�͂��̂܂܂Ȃ�
        seedIns = Instantiate(seedPrefab[seedNo + 1], target, Quaternion.identity, seedPosition);
        //���L�ł����s�\
        //seed seedIns = Instantiate(seedPrefab[seedNo + 1], target, Quaternion.identity);
        //�ЂƂ傫���I�u�W�F�N�g�ɕς���
        seedIns.seedNo = seedNo + 1;
        seedIns.isDrop = true;
        //simulated��Rigidbody2D�̋@�\
        //Simulated�̓I���ɂ���ƕ������ۂ��K������A�I�t�ɂ���ƕ������ۂ��K������Ȃ��Ȃ�܂��B
        //Rigidbody2D��ǉ������i�K�ł̓I���ɂȂ��Ă���̂ŕ������ۂ��K������܂��B
        //�I�t�ɂ���ƕ������ۂ��K������Ȃ��Ȃ�܂����A�������y���Ȃ�܂��B
        seedIns.GetComponent<Rigidbody2D>().simulated = true;
        //�I�u�W�F�N�g��\������
        seedIns.gameObject.SetActive(true);

        //Mathf.Pow�F�g���΂ׂ���i��F2��2��A�Ƃ��j���v�Z���邱�Ƃ��ł��܂��B����͔�r�I�������d���v�Z���Ƃ���Ă��܂��B
        //2��
        //�����F1�_,�����F2�_,�����΁F4�_,�΁��ԁF8�_,�ԁ����F16�_,������F32�_,�⁨���F64�_
        totalscore += (int)Mathf.Pow(2, seedNo);
        //3��
        //�����F1�_,�����F3�_,�����΁F9�_,�΁��ԁF27�_,�ԁ����F81�_,������F243�_,�⁨���F729�_
        //totalscore += (int)Mathf.Pow(3, seedNo);
        SetScore(totalscore);
    }
    private void SetScore(int score)
    {
        txtScore.text = score.ToString();
    }
    public void OnStartButton()
    {
        //StartButton���\��
        StartButton.SetActive(false);
        //RestartButton��\��
        ReStartButton.SetActive(true);
        CreateSeed();
        NextSeed();
    }
    public void OnReStartButton()
    {
        //RestartCanvas��\��
        ReStartCanvas.SetActive(true);
        RestartOnClickCount = 1;
        //�I�u�W�F�N�g���\������
        //CreateseedIns.gameObject.SetActive(false);
        //�I�u�W�F�N�g���\������
        //seedIns.gameObject.SetActive(false);
    }
    public void OnYesButton()
    {
        RestartOnClickCount = 0;
        //�ŏ�����̉�ʂɖ߂�
        SceneManager.LoadScene(0);
    }
    public void OnNoButton()
    {
        RestartOnClickCount = 0;
        //RestartCanvas���\��
        ReStartCanvas.SetActive(false);
        //�I�u�W�F�N�g���\������
        //seedIns.gameObject.SetActive(true);
        //�I�u�W�F�N�g���\������
        //CreateseedIns.gameObject.SetActive(true);
    }
}
