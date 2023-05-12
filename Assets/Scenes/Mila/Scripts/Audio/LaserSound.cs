using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
    public Transform playerTransform;
    public AudioSource laserSound;
   
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = Mathf.Log10(100f/distance);
        laserSound.volume = volume;
        
    }
}
