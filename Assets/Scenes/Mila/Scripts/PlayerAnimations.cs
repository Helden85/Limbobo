using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
     Animator animations;
      public GameObject PlayerMovement;
      

    void Start()
    {
        animations = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
         {
            
            animations.SetBool("Walk", true);
        }
        
         else
         {
            animations.SetBool("Walk", false);
         }
        
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton3))
        {
            
            animations.SetBool("Run", true);
            animations.SetBool("Walk", false);
        
        }
        else
        {
            animations.SetBool("Run", false);
        }
         if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            animations.SetBool("Jump", true);
          
        }
        else
        {
            animations.SetBool("Jump", false);

        }
        
    }
}
