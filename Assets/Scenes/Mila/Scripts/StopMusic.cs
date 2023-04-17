using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    //Stops playing the main theme once the game ends
    void Start()
    {
        GameObject music = GameObject.FindGameObjectWithTag("MusicSource");
        if (music)
        {
            Destroy(music);

        }

    }

}
