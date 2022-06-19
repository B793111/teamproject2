using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UImanager : MonoBehaviour
{
    //public GameObject gameover;
    //playercontrol playercontrol;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        //playercontrol = GameObject.Find("player").GetComponent<playercontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        
            
    }
}
