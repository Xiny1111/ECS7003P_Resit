using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D Coll;
    //ground layer
    public LayerMask Ground;
    //frog's moving boundary
    public Transform leftpoint,rightpoint;
    //frog's moving speed and jumpforce
    public float speed,jumpforce;
    //frog's moving area
    private float leftx,rightx;
    //whether the frog's face is to the left
    private bool Faceleft = true;



    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        //let the boundary disapear
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        

    }

    
    void Update()
    {
       SwitchAnim();
    }


    //frog movement
    void Movement()
    {
        if(Faceleft) //face to the left
        {
            if(Coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("jumping",true);
                rb.velocity = new Vector2(-speed,jumpforce);
            }
            if(transform.position.x <= leftx)     // turn if over the leftpoint
            {
                transform.localScale = new Vector3(-1,1,1);
                Faceleft=false;
            }
        }
        else //face to the right
        {
            if(Coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("jumping",true);
                rb.velocity = new Vector2(speed,jumpforce);
            }
            if(transform.position.x >= rightx)   // turn if over the rightpoint
            {
                transform.localScale = new Vector3(1,1,1);
                Faceleft = true;

            }
        }

    }

    //frog switch animation
    void SwitchAnim()
    {
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y<0.1)
            {
                anim.SetBool("jumping",false);
                anim.SetBool("falling",true);
            }
        }
        if (Coll.IsTouchingLayers(Ground)&& anim.GetBool("falling"))
        {
            anim.SetBool("falling",false);
        }
    }

 


}
