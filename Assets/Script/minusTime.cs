using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minusTime : MonoBehaviour
{
    public bool Waiting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf&&!Waiting)
        {
            Waiting = true;
            Invoke("ActiveFalse", 1f);
        }
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
        Waiting = false;
    }
}
