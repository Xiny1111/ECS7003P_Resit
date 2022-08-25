using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Playercontroller : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator anim;
    
    public Collider2D coll;
    public Transform ceilingCheck,groundCheck;
    //player audios
    public AudioSource jumpAudio,hurtAudio,deadAudio,CollectionsAudio;
    //player running particle effect
    public ParticleSystem playerPS;
    //player speed
    public float speed;
    //player jumpforce
    public float jumpforce;
    //ground layer
    public LayerMask Ground;
    //player health situation
    public int PlayerHealth = 10;
    //player collection's number
    public int Collections = 0 ;
    //player's grade
    public Text CollectionsNum;

    

    //deafault is false
    private bool isHurt; 
    //did player touched ground
    private bool isGround;
    //player jump twice
    private int extraJump;
    

   
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        HealthBar.HealthMax = PlayerHealth;
        HealthBar.HealthCurrent = PlayerHealth;
    }

    void FixedUpdate() 
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position,0.2f,Ground);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHurt)
       {
        Movement(); 
        }
        SwitchAnim();
        newJump();
        Crouch();
        DamagePlayer();
        
    }


     //Player moving
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");
     
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove*speed,rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(horizontalMove));
            PPS();

        }

        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection,1,1);
            PPS();
        }
    
    }


    //player jump
        void newJump()
    {
        if (isGround)
        {
            extraJump = 1 ;
        }
        if(Input.GetButtonDown("Jump") && extraJump > 0 )
        {
            rb.velocity = Vector2.up * jumpforce; // new Vector2(0,1)
            extraJump -- ;
            anim.SetBool("jumping",true);
            jumpAudio.Play();
            PPS();
        }
        if(Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * jumpforce;
            anim.SetBool("jumping",true);
            jumpAudio.Play();
            PPS();
        }

    }


    //Switch animation
    void SwitchAnim()
    {
        anim.SetBool("idle",false);

        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling",true);
        }

        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }else if (isHurt)
        {
            anim.SetBool("hurt",true);
            anim.SetFloat("running",0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurt",false);
                anim.SetBool("idle",true);
                isHurt = false;
            }

        }
        else if(coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling",false);
            anim.SetBool("idle",true);
        }
    }


    //Collider trigger ;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collect items
        if(collision.tag == "Collections")
        {
            Collections ++;
            CollectionsAudio.Play();
        
            Destroy(collision.gameObject);

           CollectionsNum .text = "X " + Collections.ToString();
        }
        if(collision.tag == "Deadline")
        {
            deadAudio.Play();
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart",2f);

        }

    }


    // Kill enemies
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag == "Enemy")  
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(anim.GetBool("falling")) 
            {
                enemy.JumpOn();    
                rb.velocity = new Vector2(rb.velocity.x,jumpforce);
                anim.SetBool("jumping",true);
                //hurt
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10,rb.velocity.y);
                PlayerHealth --;
                HealthBar.HealthCurrent = PlayerHealth;
                hurtAudio.Play();
                isHurt = true;


            }
            else if(transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10,rb.velocity.y);
                PlayerHealth --;
                HealthBar.HealthCurrent = PlayerHealth;
                hurtAudio.Play();
                isHurt = true;
            }


        }
        }  


    //player die
    void DamagePlayer()
    {

        if(PlayerHealth <= 0)
        {
            anim.SetBool("hurt",true);
            PlayerHealth = 0;
            deadAudio.Play();
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart",2f);
        }

    }


    //Player crouching
    void Crouch()
    {
        if(!Physics2D.OverlapCircle(ceilingCheck.position,0.2f,Ground))
        {
             if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching",true);
                coll.GetComponent<CapsuleCollider2D>().size = new Vector2(0.8f,0.8f); // change collider size
                
            }
            else
            {
                anim.SetBool("crouching",false);
                coll.GetComponent<CapsuleCollider2D>().size = new Vector2(1.2f,1.5f); // make collider size back
            }   
            
        }
           
    }


    //effect
    void PPS()
    {
        playerPS.Play();

    }


    //restart game
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }


    




}


