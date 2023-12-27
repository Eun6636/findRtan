using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("level", 1);  //레벨에 따른 난이도 설정
        PlayerPrefs.SetInt("ClearLevel", 0);  //현재 클리어한 레벨

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
