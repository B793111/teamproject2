using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public float movePower;
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
    public bool isdie;
    bool nohpp;
    public int score;
    bool shootspeedup;
    public int sbulletcount;
    bool shootfront;


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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("platform"))
            isGround = true;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("platform"))
            isGround = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("platform"))
            isGround = false;
    }

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

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-1, 1);
            shootfront = true;
            anim.SetBool("isrun", true);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            transform.localScale = new Vector3(1, 1);
            shootfront = false;
            anim.SetBool("isrun", true);
        }
        else
        {
            anim.SetBool("isrun", false);
        }
    }

    void playermove()
    {
        if(isdie == false)
        {
            Vector3 moveVelocity = Vector3.zero;

            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                moveVelocity = Vector3.left;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                moveVelocity = Vector3.right;
            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
            //isGround = Physics2D.OverlapCircle(groundCheck.position, check, platform);
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
        if (collision.gameObject.CompareTag("enemy") && onhit == false)
        {
            gameObject.layer = 9;
            onhit = true;
            //Debug.Log("onhit");
            OnDamaged(collision.transform.position);
            
            
        }
        if (collision.gameObject.CompareTag("die"))
        {
            //Debug.Log("!");
            gameObject.layer = 9;
            spriteRenderer.color = new Color(1, 1, 1, 0.3f);
            hp -= 1;
            transform.position = new Vector3(0, 0, 0);
            Invoke("respawn", 2);
            
        }
        if (collision.gameObject.CompareTag("hppotion") && nohpp ==false)
        {
            
            gameObject.layer = 15;
            hp += 1;
            nohpp = true;
            Debug.Log("aaaaa");
            gameObject.layer = 8;
            nohpp = false;
        }
        if (collision.gameObject.CompareTag("item"))
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
        
        //Debug.Log("!");
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

    public void die()
    {
        isdie = true;
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 0, 0, 0.2f);
    }
}
