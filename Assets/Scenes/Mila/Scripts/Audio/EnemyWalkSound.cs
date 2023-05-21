using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSound : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] AudioSource enemyWalkSound;
    private GuardPlat guardPlatScript;
    [SerializeField] GameObject GuardPlat;
      private Health healthScript;
    [SerializeField] GameObject Health;

    Transform playerTransform;
    
    void Start()
    {

        gameObject.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        guardPlatScript = GuardPlat.GetComponent<GuardPlat>();
        healthScript = Health.GetComponent<Health>();
        
    }

   
    void Update()
    {
         if (!rb2d.IsSleeping() && guardPlatScript.isDead == false && healthScript.dead == false) 

        {
            enemyWalkSound.enabled = true;
        }
        else if (guardPlatScript.isDead == true || healthScript.dead == true)
        {
            enemyWalkSound.enabled = false;
           
        }

         float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = Mathf.Log10(15f/distance);
        enemyWalkSound.volume = volume;
    }
             
}
