using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("level", 1);  //������ ���� ���̵� ����
        PlayerPrefs.SetInt("ClearLevel", 0);  //���� Ŭ������ ����

        PlayerPrefs.SetInt("BestScoreLv1",0);
        PlayerPrefs.SetInt("BestScoreLv2", 0);
        PlayerPrefs.SetInt("BestScoreLv3", 0);
        PlayerPrefs.SetInt("BestScoreLv4",0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
