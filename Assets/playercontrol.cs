using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    bool isGround;
    bool isfall; //낙하점프가능판단
    bool candoublejump;
    bool canfalldoublejump;
    public Transform groundCheck;
    public Transform gun;
    public LayerMask platform;
    public float check;
    public GameObject enemyA;
    public int dmg;
    public GameObject bulletA;
    public GameObject bulletB;
    bool reload;
    public int hp;
    bool onhit;
    bool isdie;
    bool nohpp;
    public int score;
    bool shootspeedup;
    public int sbulletcount;
    bool shootfront;

<<<<<<< Updated upstream
=======
    public GameObject itemSound;
    public GameObject jumpSound;
    public GameObject hitSound;
>>>>>>> Stashed changes

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
   
    //Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
       // StartCoroutine(repeat());

    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (bulletA.tag == "enemy")
        {
            Debug.Log("!");
            score += 1;
        }
    }*/
    // Update is called once per frame
    
    void Update()
    {
        playermotion();
        playermove();
       if(hp ==0)
        {
            isdie = true;
        }
        
       if(hp <= 0)
            {
                gameObject.layer = 9;
                spriteRenderer.color = new Color(1, 0, 0, 0.2f);
            }
       if(hp >= 6)
        {
            hp = 5;
        }
       if(sbulletcount == 0)
        {
            shootspeedup = false;
        }
        if(reload == false)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                reload = true;
                
                if(shootspeedup == false)
                {
                    shootA();
                    Debug.Log("a");
                    Invoke("delay", 0.25f);
                }
                else if(shootspeedup == true)
                {
                    shootB();
                    Debug.Log("b");
                    Invoke("delay", 0.1f);
                    sbulletcount -= 1;
                }
                
                
            
            }
           
        }
        
    }
    void delay()
    { 
        reload = false;
        //Debug.Log("!");
    }

    void playermotion()
    {
        if (isGround)
         {
         anim.SetBool("isjump", false);
         }
        else
         {
         anim.SetBool("isjump", true);
         }

          //move
        if (rigid.velocity.x > maxSpeed) 
         {
         rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
         }
        else if (rigid.velocity.x < maxSpeed * (-1)) {
         rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
         }
        //flip
        /*if (Input.GetButtonDown("Horizontal")) 
         {
         spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
         }*/
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1);
            shootfront = true;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1);
            shootfront = false;
        }
        //run
        if (rigid.velocity.normalized.x == 0) 
         {
         anim.SetBool("isrun", false);
         }
         else 
         {
          anim.SetBool("isrun", true);
          }
    }

    void playermove()
    {
        if(isdie == false)
        {
            float h = Input.GetAxisRaw("Horizontal");
            rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
            isGround = Physics2D.OverlapCircle(groundCheck.position, check, platform);
            //플레이어점프
            if (isGround)
            {
            isfall = true;
            }

            if (Input.GetButtonDown("Jump"))
            {
                if (isGround)
                {
                //Debug.Log("j");
                jump();
                candoublejump = true;
                isfall = false;
                canfalldoublejump = false;
                }
                else if (candoublejump)
                {
                jump();
                //Debug.Log("dj");
                candoublejump = false;
                isfall = false;
                 }

                else if (isfall)
                {
                jump();
                //Debug.Log("fj");
                isfall = false;
                canfalldoublejump = true;

                }

                else if (canfalldoublejump)
                {
                jump();
                //Debug.Log("fdj");
                canfalldoublejump = false;
                }

            
            }

        }
        
        

        

    }

    void jump()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        ;
        if (collision.gameObject.tag == "enemy" && onhit == false)
        {
            gameObject.layer = 9;
            onhit = true;
            hitSound.gameObject.SetActive(true);
            OnDamaged(collision.transform.position);
            
            
        }
        if (collision.gameObject.tag == "die")
        {
            gameObject.layer = 9;
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
            hp -= 1;
            transform.position = new Vector3(0, 0, 0);
            Invoke("respawn", 2);
            
        }
        if (collision.gameObject.tag == "hppotion" && nohpp ==false)
        {
            
            gameObject.layer = 15;
            hp += 1;
            nohpp = true;
            Debug.Log("aaaaa");
            gameObject.layer = 8;
            nohpp = false;
        }
        if (collision.gameObject.tag == "item")
        {
            shootspeedup = true;
            sbulletcount = 100;
            
        }
    }
    void respawn()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void OnDamaged(Vector2 targetPos)
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.3f);
        int bounce = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(bounce, 0.1f) *10, ForceMode2D.Impulse);
        hp -= 1;
        Invoke("offDamaged", 2);
    }
    void offDamaged()
    {
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        hitSound.gameObject.SetActive(false);
        onhit = false;

    }
    void shootA()
    {
        if(isdie == false)
        {
            GameObject bullet = Instantiate(bulletA, gun.position, transform.rotation);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            if(shootfront == false)
                rigid.AddForce(Vector2.right * 12, ForceMode2D.Impulse);
            else if(shootfront == true)
                rigid.AddForce(Vector2.left * 12, ForceMode2D.Impulse);

        }
       

    }
    void shootB()
    {
        if (isdie == false)
        {
            if (shootfront == false)
            {
                GameObject bullet = Instantiate(bulletB, gun.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.right * 12, ForceMode2D.Impulse);
            }
            else if (shootfront == true)
            {
                GameObject bullet = Instantiate(bulletB, gun.position, Quaternion.Euler(new Vector3(0, 0, 90)));
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.left * 12, ForceMode2D.Impulse);
            }

        }


    }
}
