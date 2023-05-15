using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Ambience : MonoBehaviour
{
      [SerializeField] Transform playerTransform;
     [SerializeField] AudioSource thunderSound;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    
    void Update()
    {
         float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = Mathf.Log10(35f/distance);
        thunderSound.volume = volume;
        
        
    }
}
