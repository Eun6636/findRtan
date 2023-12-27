using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchTxt : MonoBehaviour
{
    public bool Waiting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && !Waiting)
        {
            Waiting = true;
            Invoke("SetActiveFalse", 1f);
        }
    }

    void SetActiveFalse()
    {
        Waiting = false;
        gameObject.SetActive(false);
    }
}
