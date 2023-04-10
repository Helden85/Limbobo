using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource walkingSound;
    [SerializeField] AudioSource runningSound;
    [SerializeField] AudioSource impactSound;
    public GameObject PlayerMovement;
    private PlayerMovement script;


    void Start()
    {

        script = PlayerMovement.GetComponent<PlayerMovement>();
        impactSound = GetComponent<AudioSource>();

    }


    void Update()
    {
        PlayWalkingSound();
        PlayRunningSound();
        HittingTheGround();

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
        //Plays running sound and disables walking sound
        if (Input.GetAxis("Horizontal") != 0 && script.isGrounded == true && Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            walkingSound.enabled = false;

            runningSound.enabled = true;

        }
        else
        {

            runningSound.enabled = false;

        }

    }

    public void HittingTheGround()
{
    //Plays a thump sound when player collides with the ground
    if (script.isGrounded == true)
    {
             
            impactSound.Play();
            
    }
}

}
