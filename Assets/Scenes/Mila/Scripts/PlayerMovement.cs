using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Variables for moving 
    [SerializeField] float speed = 1f;
    [SerializeField] float runSpeed;
    float movement;
   
    //Variables for jumping mechanic
    [SerializeField] int maxJumps = 2;
    int jumps;
    [SerializeField] float jumpForce = 5f;
    public bool isGrounded;

    //Invisible walls
    [SerializeField] float xRangeLeft;
    [SerializeField] float xRangeRight;

    //Audio
    [SerializeField] AudioSource impactSound;

    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        
    }


    void Update()
    {
        
        PlayerControls();
        PlayerBoundaries();
        

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

        // Allows the player to move left and right using arrows and AD or the left joystick
        movement = Input.GetAxis("Horizontal");

        transform.Translate(Vector2.left * Time.deltaTime * speed * movement);

        //Turns player to look in the moving direction
        if (Input.GetAxis("Horizontal") > 0)
         {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
         else if(Input.GetAxis("Horizontal") < 0)
         {
            transform.localScale = new Vector3(1, 1, 1);
         }
        
        //Player can run by holding down shift or triangle button on playstation controller

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton3))
        {
            transform.Translate(Vector2.left * Time.deltaTime * runSpeed * movement);
            

        }

        //Player can jump by using either space or cross on ps4/5 controller

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
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
   
    
    //Checks if the player is touching the ground
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            jumps = maxJumps;
            isGrounded = true;
            //impactSound.Play();
            
        }    
    }

}
