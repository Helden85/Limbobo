using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
     Animator animations;
      public GameObject PlayerMovement;
       
    private PlayerMovement playerMovementScript;
    public bool isCrouched;


    void Start()
    {
        animations = GetComponent<Animator>();
        playerMovementScript = PlayerMovement.GetComponent<PlayerMovement>();
        
    }

    void Update()
    {
        WalkingAnimation();
        RunningAnimation();
        CrouchAnimation();
        SlideToCrouch();



    }
    public void WalkingAnimation()
    {
        if (Input.GetAxis("Horizontal") != 0 && playerMovementScript.isGrounded == true)
         {
            
            animations.SetBool("Walk", true);
        }
        
         else
         {
            animations.SetBool("Walk", false);
         }

    }
    public void RunningAnimation()
    {
        //Plays running animation and disables walking animation also doesnt let you do them in the air.
        if (Input.GetAxis("Horizontal") != 0 && playerMovementScript.isGrounded == true && Input.GetKey(KeyCode.RightShift) || Input.GetAxis("Horizontal") != 0
         && playerMovementScript.isGrounded == true && Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Horizontal") != 0 && playerMovementScript.isGrounded == true && Input.GetKey(KeyCode.JoystickButton4))
        {
            
            animations.SetBool("Run", true);
            animations.SetBool("Walk", false);
        
        }
        else
        {
            animations.SetBool("Run", false);
        }
         if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton1))
        {
            animations.SetBool("Jump", true);
          
        }
        else
        {
            animations.SetBool("Jump", false);

        }

    }

    public void CrouchAnimation()
    {
        if (Input.GetKey(KeyCode.U) && playerMovementScript.isCrouched == false || Input.GetKey(KeyCode.JoystickButton0) && playerMovementScript.isCrouched == false)
        {
            animations.SetBool("Crouch", true);
           
        }
        else
        {
            animations.SetBool("Crouch", false);
        }
    }
    public void SlideToCrouch()
    {
        if (Input.GetAxis("Horizontal") != 0 && isCrouched == false && Input.GetKey(KeyCode.U) ||
        Input.GetAxis("Horizontal") != 0 && isCrouched == false && Input.GetKey(KeyCode.JoystickButton0))
        {
            animations.SetBool("Slide", true);
            animations.SetBool("Crouch", true);

        }
        else
        {
            animations.SetBool("Slide", false);

        }
    }
}
