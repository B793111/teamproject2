using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycontrol : MonoBehaviour
{
   
       

    Rigidbody2D rigid;
    public int nextMove;
    public int canjump;
    public bool filpX;
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
    public float enemyatkdelay = 1;

    SpriteRenderer spriteRenderer;
    Animator anim;
    playercontrol playercontrol;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        playercontrol = GameObject.Find("Player").GetComponent<playercontrol>();
        pos = this.my.transform.position;
        
        go();

        Invoke("think", 3);

        if (pos.x < 20 && pos.x > 11)
        {
            nextMove = -1;
        }
        if (pos.x < 58 && pos.x > 54)
        {
            nextMove = -1;
        }
        if (pos.x < -20 && pos.x > -24)
        {
            nextMove = -1;
        }
        if (pos.x < -13 && pos.x > -21)
        {
            nextMove = 1;
        }
        if (pos.x < -55 && pos.x > -60)
        {
            nextMove = 1;
        }
        if (pos.x < 24 && pos.x > 21)
        {
            nextMove = 1;
        }
        if (nextMove == 1)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        if (nextMove == -1)
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);

        //target = GameObject.Find("tower").transform;

        Invoke("think", 3);
       
    } 

    // Update is called once per frame
    void Update()
    {
        
        enemymotion();

        if (enemyatkdelay > 0)//몬스터 타워 공격 쿨타임 
        {
            enemyatkdelay -= Time.deltaTime;
        }
    }
    void go()
    {
        rigid.velocity = new Vector2(nextMove * speed * 0.5f, rigid.velocity.y);
        Invoke("go", 2);
    }
    void think()
    {
        
        canjump = Random.Range(0, 2);
        
        if (canjump == 1)
        {
            rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            anim.SetBool("ejump", false);
        }
        if (canjump ==0 )
        {
            anim.SetBool("ejump", true);
        }

        Invoke("think", 3);
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
            itemrandom = Random.Range(1,10);
            if(itemrandom ==2)
            {
                GameObject enemy = Instantiate(hppotion, my.position, transform.rotation);
            }
            if(itemrandom ==3)
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
        if (collision.gameObject.tag == "tower")
        {
            OnDamaged(collision.transform.position);
            Debug.Log("!!!!!");
        }


    }

    /*void OnCollisionStay2D(Collision2D col) //몬스터 타워 공격 쿨타임
    {

        if (col.gameObject.tag == "tower")
        {

            if (enemyatkdelay <= 0)
            {
                GameObject.FindWithTag("tower").GetComponent<tower>().towerhit();
                enemyatkdelay = 3;

            }
        }
    }*/
    void OnDamaged(Vector2 targetPos)
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        int bounce = transform.position.x - targetPos.x > 0 ? 1 : -1;
        // rigid.AddForce(new Vector2(bounce * 10, 1) * 10, ForceMode2D.Impulse);
        rigid.AddForce(new Vector2(bounce, 1) * 8, ForceMode2D.Impulse);

    }

    void destroy()
    {
        Destroy(gameObject);
    }
   
}
