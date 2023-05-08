using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyClickSoundMenu : MonoBehaviour
{
    //Stops playing the main theme once the game ends
    void Start()
    {
        GameObject clickSoundMenu = GameObject.FindGameObjectWithTag("ClickSoundMenu");
        if (clickSoundMenu)
        {
            Destroy(clickSoundMenu);

        }

    }

}