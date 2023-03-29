using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource walkingSound;
    [SerializeField] AudioSource runningSound;
    public GameObject PlayerMovement;
    private PlayerMovement script;

    
    void Start()
    {
        //Finds a refrence to PlayerMovement script
        script = PlayerMovement.GetComponent<PlayerMovement>();
        

    }

   
    void Update()
    {
        PlayWalkingSound();
        PlayRunningSound();
    }
      public void PlayWalkingSound()
    {
    //Plays the walking sound when AD or arrows are pressed and player is touching the ground
         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && script.isGrounded == true)
         
            {
                walkingSound.enabled = true;
                
            }
            else
            {
                
                walkingSound.enabled = false;
            }
}
public void PlayRunningSound()
{
    //Plays running sound and disables walking sound
    if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) && script.isGrounded == true)
        {
            walkingSound.enabled = false;

            runningSound.enabled = true;
            

        }
        else
        {
            
            runningSound.enabled = false;

        }

}

}