using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
     Animator animations;
    
    void Start()
    {
        animations = GetComponent<Animator>();
        
    }

    
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
         {
            //transform.localScale = new Vector3(-1, 1, 1);
            animations.SetBool("Walk", true);
            
        }
        
         else if(Input.GetAxis("Horizontal") < 0)
         {
            //transform.localScale = new Vector3(1, 1, 1);
            animations.SetBool("Walk", true);
         }
         else
         {
            animations.SetBool("Walk", false);
         }
        
        //Player can run by holding down shift or triangle button on playstation controller

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton3))
        {
            
            animations.SetBool("Run", true);
            animations.SetBool("Walk", false);
        
         }
        else
        {
            animations.SetBool("Run", false);
        }
        
    }
}
