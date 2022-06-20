using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countscore : MonoBehaviour
{
    playercontrol playercontrol;
    public Text score;
    // Start is called before the first frame update
    void Start()
    {
        playercontrol = GameObject.Find("Player").GetComponent<playercontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score : " + playercontrol.score;
    }
}
