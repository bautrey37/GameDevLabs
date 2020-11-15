using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keybindings:
///     a/d and left/right - moving left and right
///     left shift - run
///     left control - crouch
///     space - jump
///     1 - take a long rest
/// </summary>
public class CharacterInput : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;

    private bool dead;
	// Use this for initialization
	void Start ()
	{
	    sr = GetComponent<SpriteRenderer>();
	    animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if(dead)
            return;

	    float horizontal = Input.GetAxis("Horizontal");
	    bool moving = false;

        //Move based on x axis
	    if (Math.Abs(horizontal) > float.Epsilon)
	    {
	        sr.flipX = horizontal < 0f;
	        moving = true;	        
	    }
        
        //Play death animation
	    if (Input.GetKeyDown(KeyCode.Alpha1))
	    {
	        //animator.SetTrigger("Nap");
	        dead = true;
	    }

        //Play jump animation
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        //animator.SetTrigger("Jump");
	    }

        animator.SetBool("Walk", moving);
        //animator.SetBool("Run", Input.GetKey(KeyCode.LeftShift));
        //animator.SetBool("Crouch", Input.GetKey(KeyCode.LeftControl));


    }
}
