using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip;  //�÷����� ���� ����
    public AudioSource audioSource;  //���� �� ���������� �÷��� �Ұ��̳�


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCard()
    {

        audioSource.PlayOneShot(flip); //����


        anim.SetBool("isOpen", true);

        transform.Find("back").transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").transform.Find("Canvas").gameObject.SetActive(false);

        //���࿡ �̹� �ѹ� �����ؼ� firstCard�� null�� �ƴ��� üũ
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


    //�� �ι� ���� �ϳĸ�, ���Ӹ޴������� �θ� �Լ��� �Լ������� Invike �Լ��� �� ����ϱ� ������ 

    public void destroyCard()  // �����ϴ� �Լ��� Invoke�ϴ� �Լ�
    {
        Invoke("destroyCardInvoke", 0.5f);
    }

    void destroyCardInvoke()  //������ ���� �Լ�
    {
        Destroy(gameObject);
    }

    public void closeCard()  //ī�带 ������ �Լ��� Invoke�ϴ� �Լ�
    {
        Invoke("closeCardInvoke", 0.5f);
    }

    void closeCardInvoke() //���������� ������ �Լ�
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").transform.Find("front").gameObject.SetActive(false);
        transform.Find("back").transform.Find("Canvas").gameObject.SetActive(true);
    }
}
