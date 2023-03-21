using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    void Start()
    {
        //This allows the music to continue in the next scene
        DontDestroyOnLoad(this);

    }


}
