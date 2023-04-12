using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollinEnemyHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer materialRenderer;

    float redValue = 0f;
    [SerializeField] float colorChangeSpeed;
    public bool enemyIsHit = false;
    Color takingHitColor = new Color(0, 0, 0);

    private void OnTriggerEnter2D(Collider2D weaponCollision)
    {
        if(weaponCollision.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit");
            enemyIsHit = true;
        }
    }

    void Update()
    {
        takingHitColor = new Color(redValue, 0f, 0f);
        materialRenderer.material.color = takingHitColor;

        if (enemyIsHit && redValue < 1f)
        {
            Debug.Log("redValue " + redValue);
            redValue += colorChangeSpeed * Time.deltaTime;
            if(redValue >= 1f)
            {
                enemyIsHit = false;
            }
        }

        if(!enemyIsHit && redValue >= 0f)
        {
            Debug.Log("redValue " + redValue);
            redValue -= colorChangeSpeed * Time.deltaTime;
        }
    }
}