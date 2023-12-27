using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip;  //플레이할 음악 파일
    public AudioSource audioSource;  //누가 그 음악파일을 플레이 할것이냐


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCard()
    {

        audioSource.PlayOneShot(flip); //사운드


        anim.SetBool("isOpen", true);

        transform.Find("back").transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").transform.Find("Canvas").gameObject.SetActive(false);

        //만약에 이미 한번 오픈해서 firstCard가 null이 아닌지 체크
        if (gameManager.I.firstCard == null)
        {
            gameManager.I.firstCard = gameObject;
        }
        else
        {
            gameManager.I.secondCard = gameObject;
            gameManager.I.isMatched();
        }
    }


    //왜 두번 일을 하냐면, 게임메니저에서 부를 함수와 함수내에서 Invike 함수를 또 사용하기 때문에 

    public void destroyCard()  // 삭제하는 함수를 Invoke하는 함수
    {
        Invoke("destroyCardInvoke", 0.5f);
    }

    void destroyCardInvoke()  //실질적 삭제 함수
    {
        Destroy(gameObject);
    }

    public void closeCard()  //카드를 뒤집는 함수를 Invoke하는 함수
    {
        Invoke("closeCardInvoke", 0.5f);
    }

    void closeCardInvoke() //실질적으로 뒤집는 함수
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").transform.Find("front").gameObject.SetActive(false);
        transform.Find("back").transform.Find("Canvas").gameObject.SetActive(true);
    }
}
