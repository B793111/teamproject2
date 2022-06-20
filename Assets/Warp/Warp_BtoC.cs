using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp_BtoC : MonoBehaviour
{
    public GameObject player;
    Warp_Map warp;
    int type;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        warp = GameObject.Find("Warp").GetComponent<Warp_Map>();
        type = warp.maptype;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = new Vector2(25f, -1f);
            type = 2;
            warp.maptype = type;
        }
    }
}
