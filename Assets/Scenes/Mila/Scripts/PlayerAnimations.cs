using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
     Animator animations;
      public GameObject PlayerMovement;
       
    private PlayerMovement script;
      

    void Start()
    {
        animations = GetComponent<Animator>();
        script = PlayerMovement.GetComponent<PlayerMovement>();
        
    }

    void Update()
    {
        WalkingAnimation();
        RunningAnimation();



    }
    public void WalkingAnimation()
    {
        if (Input.GetAxis("Horizontal") != 0 && script.isGrounded == true)
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
        //It looks so massive because we have 3 buttons for running
        if (Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.RightShift) || Input.GetAxis("Horizontal") != 0
         && script.isGrounded == true && Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.JoystickButton4))
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

}
