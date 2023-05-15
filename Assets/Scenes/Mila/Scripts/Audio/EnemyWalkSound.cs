using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkSound : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] AudioSource enemyWalkSound;
    private GuardPlat script;
    [SerializeField] GameObject GuardPlat;
    //  public GameObject PlayerMovement;
    // private PlayerMovement script;
    Transform playerTransform;
    //private PlayerMovement script;
    void Start()
    {

        gameObject.GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        script = GuardPlat.GetComponent<GuardPlat>();
    }

   
    void Update()
    {
         if (!rb2d.IsSleeping() && script.isDead == false)

        {
            enemyWalkSound.enabled = true;
        }
        else if (script.isDead == true)
        {
            enemyWalkSound.enabled = false;
           
        }

         float distance = Vector2.Distance(transform.position, playerTransform.position);
        float volume = Mathf.Log10(15f/distance);
        enemyWalkSound.volume = volume;
    }
             
}
