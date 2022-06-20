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
    public GameObject restart_obj;
    public GameObject main_obj;
    public GameObject Bgm;
    public Slider healthBarSlider;



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
        if (healcountstop == false)
        {
            if (healtime <= 0)
            {
                InvokeRepeating("towerheal", 0f, 1f);
                healcountstop = true;
            }
        }

        if (hp <= 0)
        {
            Gameovertext.SetActive(true);
            restart_obj.gameObject.SetActive(true);
            main_obj.gameObject.SetActive(true);
            Bgm.gameObject.SetActive(false);
            Time.timeScale = 0f;
            GameObject die = GameObject.Find("player") as GameObject;
            die.GetComponent<playercontrol>().die(); //playercomtrol�� dieȣ��
        }
    }

    void OnCollisionEnter2D(Collision2D col) //���� Ÿ�� ���� 
    {
        

        if (col.gameObject.tag == "enemy")
        {
            towerhit();
            Debug.Log("@@@@");
            //GameObject.FindWithTag("enemy").GetComponent<enemycontrol>().towerhit();
        }
    }

    public void towerhit()
    {
        CancelInvoke("towerheal");
        healtime = 5;
        healcountstop = false;
        warningtext.SetActive(true);
        hp -= 3;
        healthBarSlider.value -= 3;

    }
    public void towerheal()
    {
        if(hp <= 100)
        {
            warningtext.SetActive(false);
            hp += 2;
            healthBarSlider.value += 2;
            healcountstop = true;
        }

    }
}
