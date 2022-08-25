using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator anim;
    //enemy died music
    protected AudioSource deathAudio;
    //player's health value
    private Playercontroller playerHealth;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
        
    }

    //enemy destroyed
    public void Death()
    {
        Destroy(gameObject);
    }

    //player killed enemy
    public void JumpOn()
    {
        anim.SetTrigger("death");
        deathAudio.Play();
    }

 
}
