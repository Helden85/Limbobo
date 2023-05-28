using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDoesDamage : MonoBehaviour
{
 
      private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //collision.GetComponent<Health>().TakeDamage(4);
            collision.GetComponent<Health>().TakeDamage(1);
            
        }
    }
}
