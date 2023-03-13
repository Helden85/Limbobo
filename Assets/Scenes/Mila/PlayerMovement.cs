using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for moving 
    public float speed = 1f;
    private float movement;

    //Variables for jumping mechanic
    public int maxJumps = 2;
    private int jumps;
    private float jumpForce = 5f;
    public bool grounded;
    public float movespeed;



    void Start()
    {

    }


    void Update()
    {
        // Allows the player to move left and right using arrows and AD
        movement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.left * Time.deltaTime * speed * movement);


        //Player can jump by using space

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Jump();
        }

    }

    //Allows the player to jump twice
    public void Jump()
    {
        if (jumps > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            grounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }

    //Checks if the player is touching the ground
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            jumps = maxJumps;
            grounded = true;
            movespeed = 2.0F;
        }

    }

}

