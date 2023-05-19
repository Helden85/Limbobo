using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderSound : MonoBehaviour
{
    [SerializeField]Rigidbody2D rb2d;
    [SerializeField] AudioSource boulderSound;
    Vector2 previousPosition;
    bool stopSound;
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        previousPosition = transform.position;
    }

    
    void Update()
    {
       
         StartCoroutine(DisableBoulderSound());

    }
    
     public void PlayBoulderSound()
    {

        if (!rb2d.IsSleeping() && stopSound == false)

        {
            boulderSound.enabled = true;
        }
        else
        {
            boulderSound.enabled = false;
        }
    }

     IEnumerator DisableBoulderSound()
    {
        
        yield return new WaitForSeconds(1.0f);
        PlayBoulderSound();
    }

     public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "StopTheRock")
        {
            stopSound = true;

        }    
    }
}
