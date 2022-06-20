using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAspawn : MonoBehaviour
{
    public GameObject enemyA;
    public GameObject enemyB;
    public GameObject enemyBoss;
    Rigidbody2D rigid;
    public GameObject spawnwhat;
    public Transform portal;
    public int check;
   
    

    IEnumerator enemyspawn()
    {
        check = Random.Range(0, 2);
        if(check ==1)
        {
            spawnwhat = enemyA;
        }
        if(check < 1)
            {
            spawnwhat = enemyB;
        }
        GameObject enemy = Instantiate(spawnwhat, portal.position, transform.rotation);
        yield return new WaitForSeconds(15);
        StartCoroutine(enemyspawn());
    }
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(enemyspawn());
        Invoke("boss", 10);
    }

    // Update is called once per frame
    void Update()
    {
        //rigid.velocity = new Vector2(nextMove * faster, rigid.velocity.y);
    }
    void boss()
    {
        Debug.Log("www");
        GameObject enemy = Instantiate(enemyBoss, portal.position, transform.rotation);
        Invoke("boss", 25);
    }
}
