using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvChoice : MonoBehaviour
{
    public int SelfLv; //퍼블릭으로 작성
    int ClearLevel;

    void Start()
    {
        ClearLevel = PlayerPrefs.GetInt("ClearLevel");
        if (SelfLv <= ClearLevel+1) 
        {
            gameObject.GetComponent<Image>().color = Color.white;  //활성화 된다면 색을 하얀색으로
            gameObject.transform.Find("txt").GetComponent<Text>().text = SelfLv.ToString(); //자신의 레벨 
        }
    }

    void Update()
    {
        
    }

    public void GameStart()
    {
        

        if (SelfLv <= ClearLevel + 1 || SelfLv ==1) 
        {
            PlayerPrefs.SetInt("Level", SelfLv); // 선택한 레벨정보를 건내주기
            SceneManager.LoadScene("MainScene");
        }
        
    }
}
