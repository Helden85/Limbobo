using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSound : MonoBehaviour
{
    // Audio
    public AudioSource jumpsound;

    public AudioClip jumpsoundclip;

    void Start()
    {
        jumpsound.PlayOneShot(jumpsoundclip);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpsound.PlayOneShot(jumpsoundclip);
        }
    }
}
