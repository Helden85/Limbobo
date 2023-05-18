using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource walkingSound;
    [SerializeField] AudioSource runningSound;
    [SerializeField] AudioSource crouchSound;
    


    public GameObject PlayerMovement;
    private PlayerMovement script;



    void Start()
    {

        script = PlayerMovement.GetComponent<PlayerMovement>();
        
        

    }


    void Update()
    {
        PlayWalkingSound();
        PlayRunningSound();
        PlayCrouchSound();
      


    }
    public void PlayWalkingSound()
    {
        //Plays the walking sound when AD or arrows are pressed and player is touching the ground

        if (Input.GetAxis("Horizontal") != 0 && script.isGrounded == true)

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
        //Plays running sound and disables walking sound (it looks like this because there are 3 buttons for running)
        if (Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.RightShift) || Input.GetAxis("Horizontal") != 0 && script.isGrounded == true 
        && Input.GetKey(KeyCode.LeftShift) || Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.JoystickButton4))
        {
            walkingSound.enabled = false;

            runningSound.enabled = true;

        }
        else
        {

            runningSound.enabled = false;

        }

    }
    public void PlayCrouchSound()
    {
        if (Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.U)
        || Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.JoystickButton0))
        {
            walkingSound.enabled = false;

            crouchSound.enabled = true;

        }
        else
        {

            crouchSound.enabled = false;

        }

    }

    public void PlayGruntSound()
    {

    }


}
