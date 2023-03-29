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


    void Update()
    {
        /*CM vcam2 moves to the left till it hits 0 on the x axis then
        it moves to CM vcam1 to follow the player*/
        {
            if (transform.position.x > cameraDestination)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                //Debug.Log("player dont move");

            }
            else if (transform.position.x <= 0)
            {

                vCam2.SetActive(false);

            }


        }

    }
}
