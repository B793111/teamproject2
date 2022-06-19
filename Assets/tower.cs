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
    public Slider healthBarSlider;

    public float enemyatkdelay = 3;


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
        if(healcountstop==false)
        {
            if (healtime <= 0)
            {
                InvokeRepeating("towerheal", 0f, 1f);
                healcountstop = true;
            }
        }

        if (enemyatkdelay > 0)//몬스터 타워 공격 쿨타임 
        {
            enemyatkdelay -= Time.deltaTime;
        }
        if (hp <= 0)
        {
           Gameovertext.SetActive(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col) //몬스터 타워 공격 쿨타임
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
