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

    //점수계산
    public Text N_Score;
    public Text B_Score;
    int N_ScoreInt;
    int B_ScoreInt;

    //성공 실패 텍스트
    public GameObject Success;
    public GameObject Failed;

    public GameObject minusTime;   //타임감소

    public GameObject endTxt;

    //레벨 설정
    float time = 60f;
    int[] rtans;

    public GameObject card;

    public static gameManager I;

    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip fail;

    int MathNum;


    
    public int PlayerClearLv = 0; //플레이어가 클리어한 레벨

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


        StageLv(); // 레벨에따른 화면 초기화

    }

    void Update()
    {
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if(time <=0) 
        {
            endTxt.SetActive(true);  //퍼블릭으로 ReGame 넣음
            Time.timeScale = 0.0f;
        }

        if(time <=10)//남은시간 10초 긴박한 브금 및 글자색
        {
            timeTxt.GetComponent<Text>().color = Color.red;
            audioManager.GetComponent<audioManager>().backMusicpitch();
        }


    }


    public void isMatched()
    {
        MathNum += 1;  //매치 증가
        Mathed.text = MathNum.ToString();


        //카드의 이름 비교하기 위한 초기화
        string firstCardImage = firstCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage) //같을경우
        {
            //텍스트
            Success.SetActive(true);

            audioSource.PlayOneShot(match);  //오디오

            

            firstCard.GetComponent<card>().destroyCard();
            secondCard.GetComponent<card>().destroyCard();

            int cardsLeft = GameObject.Find("Cards").transform.childCount;

            //현재 이 if문이 불릴땐 아직 Invoke 함수처리가 안되있기 때문에 2개일때 불리면 끝으로 판정한다
            if (cardsLeft == 2)
            {
                //점수 계산
                Score(time,MathNum);
                BestScore();//베스트 스코어 갱신
                N_Score.text = N_ScoreInt.ToString();
                B_Score.text = B_ScoreInt.ToString();

                //클리어 레벨을 Stage 씬에게 넘겨주기

                int SelfLV = PlayerPrefs.GetInt("Level"); //현재레벨
                int ClearLV = PlayerPrefs.GetInt("ClearLevel"); //이제까지 클리어한 레벨

                if(SelfLV> ClearLV)
                {
                    PlayerPrefs.SetInt("ClearLevel", SelfLV);
                }



                endTxt.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else //다를경우
        {
            //카드 뒷면에 접근해서 색 변경
            firstCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f, 1f);
            secondCard.transform.Find("back").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0.5f, 1f);

            //타임 2f감소
            time -= 2f;
            minusTime.SetActive(true);

            //텍스트
            Failed.SetActive(true);

            //오디오
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

        //르탄 섞기


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
                    //----------------------------------------------------------------- 위 까진 그대로


                    float x = (i / 2) * 1.4f - 2.1f;
                    float y = (i % 2) * 1.4f - 3.0f;
                    if (i < 4)
                    {
                        newCard.transform.position = new Vector3(x, y, 0);
                    }
                    else
                    {
                        //4개 이상부턴 y값 확 높이기
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
                        //중간을 비우기 위해 2개의 배치 부분 없애기
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

                    //기본 배치
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

                    //기본 배치
                    GameObject newCard = Instantiate(card);
                    newCard.transform.parent = GameObject.Find("Cards").transform;

                    //카드 크기부터 줄이자
                    newCard.transform.localScale = new Vector3(1, 1, 1);

                    float x = (i / 5) * 1.1f - 2.1f;
                    float y = (i % 5) * 1.1f - 3.0f;
                    newCard.transform.position = new Vector3(x, y, 0);

                    string rtanName = "rtan" + rtans[rtans[i]].ToString(); //배열 안의 숫자와 맞춰서 가져오기
                    newCard.transform.Find("back").transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

                }
                break;
        }
        }
    
    void Score(float time, int Mathed)  //*
    {
        //소수 부분을 내림하고 계산하는 함수
        int a = Mathf.FloorToInt(time) * 200;
        int b = Mathed * -50;
        N_ScoreInt = a + b;
    }

    void BestScore()  //* 베스트 스코어 갱신
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
