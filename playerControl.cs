﻿using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
	public float moveSpeed;
	public float jumpHeight;

	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;
	private bool doubleJumped;

		// Use this for initialization
	void Start () {
		
	}

	void FixedUpdate() 
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}

	// Update is called once per frame
	void Update () {

		if (grounded) 
		{
			doubleJumped =  false;
		}

		if (Input.GetKeyDown (KeyCode.Space) && grounded )
		{
			//GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
			Jump();
		}
		

		if (Input.GetKeyDown (KeyCode.Space) && !doubleJumped && !grounded)
		{
			//GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
			Jump();
			doubleJumped =  true;
		}

		if (Input.GetKey (KeyCode.D) )
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
			//transform.localScale += new Vector3(1.0F, 0, 0);
		}

		if (Input.GetKey (KeyCode.A) )
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed,GetComponent<Rigidbody2D>().velocity.y);
			//transform.localScale += new Vector3(-1.0F, 0, 0);
		}


	}


	public void Jump()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,jumpHeight);
	}
}
