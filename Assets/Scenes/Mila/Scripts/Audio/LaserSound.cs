using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] AudioSource laserSound;
   
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = Mathf.Log10(35f/distance);
        laserSound.volume = volume;
        
        
    }
}
