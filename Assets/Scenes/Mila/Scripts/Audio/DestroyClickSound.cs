using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClickSound : MonoBehaviour
{
    //Stops playing the main theme once the game ends
    void Start()
    {
        GameObject clickSound = GameObject.FindGameObjectWithTag("ClickSound");
        if (clickSound)
        {
            Destroy(clickSound);

        }

    }

}