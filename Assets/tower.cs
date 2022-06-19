using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tower : MonoBehaviour
{
    public int hp = 100;
    public float healtime = 5; //ȸ�� ��Ÿ��(x�ʵ��� ���� �ȹ�����)
    public float healdelay = 0; //ȸ�� ������(�ʴ� ȸ��)
    bool healcountstop = false; //ȸ�� �� ��Ÿ�� ����

    public GameObject warningtext;
    public GameObject Gameovertext;//ǥ���� �ؽ�Ʈ����
    public Slider healthBarSlider;

    public float enemyatkdelay = 3;


    void Start()
    {
        warningtext.SetActive(false); //���۽� �ؽ�Ʈ �����
    }


    void Update()
    {
        if (healtime > 0)
        {
            healtime -= Time.deltaTime;
        }
        if(healcountstop==false)
        {
            if (healtime <= 0)
            {
                InvokeRepeating("towerheal", 0f, 1f);
                healcountstop = true;
            }
        }

        if (enemyatkdelay > 0)//���� Ÿ�� ���� ��Ÿ�� 
        {
            enemyatkdelay -= Time.deltaTime;
        }
        if (hp <= 0)
        {
           Gameovertext.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col) //���� Ÿ�� ���� ��Ÿ��
    {
        

        if (col.gameObject.tag == "enemy")
        {
            towerhit();
            Debug.Log("@@@@");
            //GameObject.FindWithTag("enemy").GetComponent<enemycontrol>().towerhit();
            if (enemyatkdelay <= 0)
            {
                
                enemyatkdelay = 3;
                

            }
        }
    }

    public void towerhit()
    {
        CancelInvoke("towerheal");
        healtime = 5;
        healcountstop = false;
        warningtext.SetActive(true);
        hp -= 5;
        healthBarSlider.value -= 5;

    }
    public void towerheal()
    {
        if(hp <= 100)
        {
            warningtext.SetActive(false);
            hp += 1;
            healthBarSlider.value += 1;
            healcountstop = true;
        }

    }
}
