using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reloadBar : MonoBehaviour
{
    public Slider reloadbar;
    playercontrol playercontrol;
    // Start is called before the first frame update
    void Start()
    {
        playercontrol = GameObject.Find("player").GetComponent<playercontrol>();
    }

    // Update is called once per frame
    void Update()
    {
        reloadbar.value = playercontrol.sbulletcount;
    }
}
