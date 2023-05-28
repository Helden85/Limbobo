using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakyGround : MonoBehaviour
{
    [SerializeField] GameObject platform;
   
   
    private void OnTriggerEnter2D(Collider2D other)
    {

        
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DestroyPlatform());
        }
            
        
    }
    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(platform);
    }
}

