using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    

    public Text timeTxt;
    public Text Mathed;

    //�������
    public Text N_Score;
    public Text B_Score;
    int N_ScoreInt;
    int B_ScoreInt;

    //���� ���� �ؽ�Ʈ
    public GameObject Success;
    public GameObject Failed;

    public GameObject minusTime;   //Ÿ�Ӱ���

    public GameObject endTxt;

    //���� ����
    float time = 60f;
    int[] rtans;

    public GameObject card;

    public static gameManager I;

    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip fail;

    int MathNum;


    
    public int PlayerClearLv = 0; //�÷��̾ Ŭ������ ����

    //--------------------------------

    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject audioManager;

    void Awake()
    {
        I = this;

        
    }




    void Start()
    {

        MathNum = 0;
        Time.timeScale = 1.0f;


        StageLv(); // ���������� ȭ�� �ʱ�ȭ

    }

    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if(time <=0) 
        {
            endTxt.SetActive(true);  //�ۺ����� ReGame ����
            Time.timeScale = 0.0f;
        }

        if(time <=10)//�����ð� 10�� ����� ��� �� ���ڻ�
        {
            timeTxt.GetComponent<Text>().color = Color.red;
            audioManager.GetComponent<audioManager>().backMusicpitch();
        }


    }


    public void isMatched()
    {
        MathNum += 1;  //��ġ ����
        Mathed.text = MathNum.ToString();


        //ī���� �̸� ���ϱ� ���� �ʱ�ȭ
        string firstCardImage = firstCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage) //�������
        {
            //�ؽ�Ʈ
            Success.SetActive(true);

            audioSource.PlayOneShot(match);  //�����

            

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;

            //���� �� if���� �Ҹ��� ���� Invoke �Լ�ó���� �ȵ��ֱ� ������ 2���϶� �Ҹ��� ������ �����Ѵ�
            if (cardsLeft == 2)
            {
                //���� ���
                Score(time,MathNum);
                BestScore();//����Ʈ ���ھ� ����
                N_Score.text = N_ScoreInt.ToString();
                B_Score.text = B_ScoreInt.ToString();

                //Ŭ���� ������ Stage ������ �Ѱ��ֱ�

                int SelfLV = PlayerPrefs.GetInt("Level"); //���緹��
                int ClearLV = PlayerPrefs.GetInt("ClearLevel"); //�������� Ŭ������ ����

                if(SelfLV> ClearLV)
                {
                    PlayerPrefs.SetInt("ClearLevel", SelfLV);
                }



                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else //�ٸ����
        {
            //ī�� �޸鿡 �����ؼ� �� ����
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f, 1f);
            secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f, 1f);

            //Ÿ�� 2f����
            time -= 2f;
            minusTime.SetActive(true);

            //�ؽ�Ʈ
            Failed.SetActive(true);

            //�����
            audioSource.PlayOneShot(fail);

            //------------------------------------------------------------- 
            firstCard.GetComponent<card>().closeCard();
            secondCard.GetComponent<card>().closeCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    //-------------------------------------------------- 231227


    void StageLv()
    {
        int LV = PlayerPrefs.GetInt("Level");

        //��ź ����


        switch (LV)
        {
            case 1:

                time = 30;

                rtans = new int[] { 0, 0, 1, 1, 2, 2, 3, 3 };
                rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
                for (int i = 0; i < 8; i++)
                {

                    GameObject newCard = Instantiate(card);
                    newCard.transform.parent = GameObject.Find("Cards").transform;
                    //----------------------------------------------------------------- �� ���� �״��


                    float x = (i / 2) * 1.4f - 2.1f;
                    float y = (i % 2) * 1.4f - 3.0f;
                    if (i < 4)
                    {
                        newCard.transform.position = new Vector3(x, y, 0);
                    }
                    else
                    {
                        //4�� �̻���� y�� Ȯ ���̱�
                        y += 2 * 1.4f;
                        newCard.transform.position = new Vector3(x, y, 0);
                    }

                    string rtanName = "rtan" + rtans[i].ToString();
                    newCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

                }

                break;
            case 2:

                time = 40;
                rtans = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4};
                rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

                for (int i = 0; i < 10; i++)
                {


                    GameObject newCard = Instantiate(card);
                    newCard.transform.parent = GameObject.Find("Cards").transform;

                    if (i < 5)
                    {
                        float x = (i / 4) * 1.4f - 1.4f;
                        float y = (i % 4) * 1.4f - 3.0f;
                        newCard.transform.position = new Vector3(x, y, 0);
                    }
                    else
                    {
                        //�߰��� ���� ���� 2���� ��ġ �κ� ���ֱ�
                        float x = ((i + 2) / 4) * 1.4f - 1.4f;
                        float y = ((i + 2) % 4) * 1.4f - 3.0f;
                        newCard.transform.position = new Vector3(x, y, 0);
                    }

                    string rtanName = "rtan" + rtans[rtans[i]].ToString();
                    newCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

                }

               
                break;
            case 3:

                time = 50;

                rtans = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
                rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

                for (int i = 0; i < 16; i++)
                {

                    //�⺻ ��ġ
                    GameObject newCard = Instantiate(card);
                    newCard.transform.parent = GameObject.Find("Cards").transform;

                    float x = (i / 4) * 1.4f - 2.1f;
                    float y = (i % 4) * 1.4f - 3.0f;
                    newCard.transform.position = new Vector3(x, y, 0);

                    string rtanName = "rtan" + rtans[rtans[i]].ToString();
                    newCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

                }
                break;
            case 4:

                time = 80;
                rtans = new int[] { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 0, 0, 2, 2, 4, 4, 5, 5 };
                rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

                for (int i = 0; i < 24; i++)
                {

                    //�⺻ ��ġ
                    GameObject newCard = Instantiate(card);
                    newCard.transform.parent = GameObject.Find("Cards").transform;

                    //ī�� ũ����� ������
                    newCard.transform.localScale = new Vector3(1, 1, 1);

                    float x = (i / 5) * 1.1f - 2.1f;
                    float y = (i % 5) * 1.1f - 3.0f;
                    newCard.transform.position = new Vector3(x, y, 0);

                    string rtanName = "rtan" + rtans[rtans[i]].ToString(); //�迭 ���� ���ڿ� ���缭 ��������
                    newCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

                }
                break;
        }
        }
    
    void Score(float time, int Mathed)  //*
    {
        //�Ҽ� �κ��� �����ϰ� ����ϴ� �Լ�
        int a = Mathf.FloorToInt(time) * 200;
        int b = Mathed * -50;
        N_ScoreInt = a + b;
    }

    void BestScore()  //* ����Ʈ ���ھ� ����
    {
        int LV = PlayerPrefs.GetInt("Level");

        switch(LV)
        {
            case 1:
                B_ScoreInt = PlayerPrefs.GetInt("BestScore1");
                if (B_ScoreInt < N_ScoreInt)
                {
                    PlayerPrefs.SetInt("BestScore1", N_ScoreInt);
                    B_ScoreInt = N_ScoreInt;
                }
                break;
            case 2:
                B_ScoreInt = PlayerPrefs.GetInt("BestScore2");
                if (B_ScoreInt < N_ScoreInt)
                {
                    PlayerPrefs.SetInt("BestScore2", N_ScoreInt);
                    B_ScoreInt = N_ScoreInt;
                }
                break;
            case 3:
                B_ScoreInt = PlayerPrefs.GetInt("BestScore3");
                if (B_ScoreInt < N_ScoreInt)
                {
                    PlayerPrefs.SetInt("BestScore3", N_ScoreInt);
                    B_ScoreInt = N_ScoreInt;
                }
                break;
            case 4:
                B_ScoreInt = PlayerPrefs.GetInt("BestScore4");
                if (B_ScoreInt < N_ScoreInt)
                {
                    PlayerPrefs.SetInt("BestScore4", N_ScoreInt);
                    B_ScoreInt = N_ScoreInt;
                }
                break;



        }

        
    }


    

}
