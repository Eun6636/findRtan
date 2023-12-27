using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvChoice : MonoBehaviour
{
    public int SelfLv; //�ۺ����� �ۼ�
    int ClearLevel;

    void Start()
    {
        ClearLevel = PlayerPrefs.GetInt("ClearLevel");
        if (SelfLv <= ClearLevel+1) 
        {
            gameObject.GetComponent<Image>().color = Color.white;  //Ȱ��ȭ �ȴٸ� ���� �Ͼ������
            gameObject.transform.Find("txt").GetComponent<Text>().text = SelfLv.ToString(); //�ڽ��� ���� 
        }
    }

    void Update()
    {
        
    }

    public void GameStart()
    {
        

        if (SelfLv <= ClearLevel + 1 || SelfLv ==1) 
        {
            PlayerPrefs.SetInt("Level", SelfLv); // ������ ���������� �ǳ��ֱ�
            SceneManager.LoadScene("MainScene");
        }
        
    }
}
