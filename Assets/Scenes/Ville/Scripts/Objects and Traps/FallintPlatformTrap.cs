using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallintPlatformTrap : MonoBehaviour
{
    private float fallDelay = 0.5f;
    private float destroyDelay = 3;

    [SerializeField] private Rigidbody2D rb2d;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
