using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : MonoBehaviour
{
    bool vanish = false;

    private void Update()
    {
        if(vanish)
        {
            StartCoroutine(Vanish());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Player"))
        {
            vanish = true;
        }
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(1);
        //GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
