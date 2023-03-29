using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAudio : MonoBehaviour
{
    [SerializeField] AudioSource music = null;

    
    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
    }

    
    void Update()
    {
        

    }
    public void ToggleMusicOn()
    {
        //Toggles music on and off
        music.mute = !music.mute;

    }
}
