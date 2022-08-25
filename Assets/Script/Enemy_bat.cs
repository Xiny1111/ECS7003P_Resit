using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bat :Enemy
{
    
    private Rigidbody2D rb;
    //bat's fly area top and down boundary
    public Transform top,buttom;
    //bat's fly speed
    public float speed;
    //bat's flying area
    private float TopY,ButtomY;
    //bat's flying
    private bool isUp = true;


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        TopY = top.position.y;
        ButtomY = buttom.position.y;
        Destroy(top.gameObject);
        Destroy(buttom.gameObject);

    }

    
    void Update()
    {
        Movement();
    }

    ////bat's movement
    void Movement()
    {
        if(isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x,speed);
            if(transform.position.y > TopY)
            {
               isUp = false;
            }
        }
        else
        {
             rb.velocity = new Vector2(rb.velocity.x,-speed);
             if(transform.position.y < ButtomY)
             {
                isUp = true;
             }
        }
    }
}
