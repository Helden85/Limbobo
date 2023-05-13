using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSound : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] AudioSource enemyWalkSound;
    Transform playerTransform;
    //private PlayerMovement script;
    void Start()
    {

        gameObject.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //script = PlayerMovement.GetComponent<PlayerMovement>();
    }

   
    void Update()
    {
         if (!rb2d.IsSleeping())

        {
            enemyWalkSound.enabled = true;
        }
        else
        {
            enemyWalkSound.enabled = false;
        }

         float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = Mathf.Log10(15f/distance);
        enemyWalkSound.volume = volume;
    }
             
}
