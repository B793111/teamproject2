using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyenemy : MonoBehaviour
{

    Rigidbody2D rigid;
    public int speed;
    public int score;
    public int health;
    public GameObject effect;
    public Transform my;
    public int itemrandom;
    public GameObject hppotion;
    public int getscore;
    public GameObject item;
    public Transform target;

    SpriteRenderer spriteRenderer;
    Animator anim;
    playercontrol playercontrol;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playercontrol = GameObject.Find("player").GetComponent<playercontrol>();

        //target = GameObject.Find("tower").transform;

        Invoke("think", 2);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
        rigid.velocity=(direction * speed);

        enemymotion();
    }
    void think()
    {
        speed = Random.Range(2, 3);

        Invoke("think", 2);
    }
    void enemymotion()
    {
        if (rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("erun", false);
        }
        else
        {
            anim.SetBool("erun", true);
        }
    }
    void OnHit()
    {
        health -= 1;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OnHitcolor", 1);
        if (health <= 0)
        {
            itemrandom = Random.Range(2, 5);
            if (itemrandom == 2)
            {
                GameObject enemy = Instantiate(hppotion, my.position, transform.rotation);
            }
            if (itemrandom == 3)
            {
                GameObject enemy = Instantiate(item, my.position, transform.rotation);
            }

            playercontrol.score += getscore;
            // gameObject.layer = 14;
            // spriteRenderer.color = new Color(1, 1, 1, 0);
            GameObject a = Instantiate(effect, my.position, transform.rotation);
            Destroy(gameObject);


        }
    }
    void OnHitcolor()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {

            // playercontrol playercontrol = collision.gameObject.GetComponent<playercontrol>();

            OnHit();

        }
        if (collision.gameObject.tag == "die")
        {
            //Debug.Log("1");
            Destroy(gameObject);
        }


    }
    void destroy()
    {
        Destroy(gameObject);
    }

}
