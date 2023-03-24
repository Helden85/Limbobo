using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for moving 
    [SerializeField] float speed = 1f;
    [SerializeField] float runSpeed;
    float movement;


    //Variables for jumping mechanic
    [SerializeField] int maxJumps = 2;
    int jumps;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] bool isGrounded;

    [SerializeField] AudioSource walkingSound;
    //Invisible walls
    [SerializeField] float xRangeLeft;
    [SerializeField] float xRangeRight;



    void Start()
    {

    }


    void Update()
    {
        PlayerControls();
        PlayerBoundaries();
        PlayWalkingSound();

    }

    //Allows the player to jump twice
    public void Jump()
    {
        if (jumps > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }

    public void PlayerControls()
    {
        // Allows the player to move left and right using arrows and AD
        movement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.left * Time.deltaTime * speed * movement);

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector2.left * Time.deltaTime * runSpeed * movement);


        }
       
        //Player can jump by using space

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Jump();
        }

    }

    public void PlayerBoundaries()
    {
        //Invisible walls on the horizontal axis

        if (transform.position.x < xRangeLeft)
        {
            transform.position = new Vector2(xRangeLeft, transform.position.y);

        }
        if (transform.position.x > xRangeRight)
        {
            transform.position = new Vector2(xRangeRight, transform.position.y);

        }

    }
     public void PlayWalkingSound()
    {
    //Plays the walking sound when AD or arrows are pressed and player is touching the ground
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && isGrounded == true)
         
            {
                walkingSound.enabled = true;
                
            }
            else
            {
                
                walkingSound.enabled = false;
            }
}
    
        

    //Checks if the player is touching the ground
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            jumps = maxJumps;
            isGrounded = true;
            
        }

        
    }

}
