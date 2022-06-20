using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    public GameObject player;
    Transform moving;
    Warp_Map warp;
    int map;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Use this for initialization
    void Start()
    {
        moving = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        warp = GameObject.Find("Warp").GetComponent<Warp_Map>();
        map = warp.maptype;
        transform.position = Vector3.Lerp(transform.position, moving.position, 3f * Time.deltaTime);
        transform.Translate(0, 0, -1);
        Vector3 CameraPosition = transform.position;
        if (map == 0)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), Mathf.Clamp(transform.position.y, 2.5f, 13.5f), -10);
        if (map == 1)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -47.5f, -32.5f), Mathf.Clamp(transform.position.y, 2.5f, 13.5f), -10);
        if (map == 2)
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, 32.5f, 47.5f), Mathf.Clamp(transform.position.y, 2.5f, 13.5f), -10);
    }
}
