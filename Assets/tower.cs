using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tower : MonoBehaviour
{
    public int hp = 100;
    public float healtime = 5; //회복 쿨타임(x초동안 공격 안받으면)
    public float healdelay = 0; //회복 딜레이(초당 회복)
    bool healcountstop = false; //회복 시 쿨타임 멈춤

    public GameObject warningtext;
    public GameObject Gameovertext;//표시할 텍스트설정
    public GameObject restart_obj;
    public GameObject main_obj;
    public GameObject Bgm;
    public Slider healthBarSlider;



    void Start()
    {
        warningtext.SetActive(false); //시작시 텍스트 숨기기
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
            die.GetComponent<playercontrol>().die(); //playercomtrol의 die호출
        }
    }

    void OnCollisionEnter2D(Collision2D col) //몬스터 타워 공격 
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
