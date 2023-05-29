using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private float fallDelay = 0.5f;
    private float destroyDelay = 3;
    [SerializeField] GameObject spike;

    [SerializeField] private Rigidbody2D rb2d;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        Destroy(spike, destroyDelay);
    }
}
