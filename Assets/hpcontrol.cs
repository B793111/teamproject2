using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpcontrol : MonoBehaviour
    
{

    playercontrol playercontrol;
   // public int hp;
    public int health;
    public int heartnumer;
    public Sprite fullheart;
    public Sprite emptyheart;
    public Image[] hearts;
    public GameObject gameoverObject;
    public GameObject restartObject;
    public GameObject mainObject;


    // Start is called before the first frame update
    void Start()
    {
       playercontrol= GameObject.Find("player").GetComponent<playercontrol>();
        
    }

    // Update is called once per frame

    void Update()
    {
        for (int i =0; i<hearts.Length; i++)
        {
            if (i < playercontrol.hp)
            {
                hearts[i].sprite = fullheart;
            }
            else
            {
                hearts[i].sprite = emptyheart;
            }
        


            if (i < heartnumer)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if (playercontrol.hp <= 0)
            {
                Debug.Log("!");
                gameoverObject.gameObject.SetActive(true);
                restartObject.gameObject.SetActive(true);
                mainObject.gameObject.SetActive(true);
            }
        }
        //Debug.Log(playercontrol.hp);
        /*if(playercontrol.hp == 1)
        {
            GameObject hp = Instantiate(hpfull, UI.position, transform.rotation);
        }*/
    }
}
