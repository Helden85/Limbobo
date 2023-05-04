using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FromRightToLeft : MonoBehaviour
{

    //Variables
    [SerializeField] float speed = 2.0f;
    [SerializeField] float cameraDestination;
    [SerializeField] GameObject vCam2;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] SoundManager soundManager;


    void Update()
    {
        /*CM vcam2 moves to the left till it hits 0 on the x axis then
        it moves to CM vcam1 to follow the player*/
        {
            if (transform.position.x > cameraDestination)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                playerMovement.enabled = false;
                soundManager.enabled = false;

            }
            else if (transform.position.x <= 0)
            {

                vCam2.SetActive(false);
                playerMovement.enabled = true;
                soundManager.enabled = true;

            }


        }

    }
}