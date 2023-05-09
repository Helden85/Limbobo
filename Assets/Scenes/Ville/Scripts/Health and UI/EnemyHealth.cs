using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth; //{ get; set; }
    private Animator anim;
    public bool dead;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Boss Parameters")]
    public bool isInvulnerable = false;

    [Header("Enemy Parameters")]
    public bool enemyDead = false;
    public GameObject bossEnemy;

    [Header("Player Fetch Animations")]
    public GameObject animatedPlayer;

    [Header("Player Fetch Booleans")]
    public bool playerDead = false;

    [Header("Enemy Fetch Animations")]
    public GameObject animatedEnemy;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
            return;

        if (bossEnemy && currentHealth <= 4)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (gameObject.CompareTag("Enemy") && currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
            //animatedEnemy.GetComponent<Animator>().SetTrigger("Hurt");
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            if (!dead)
            {
                anim.SetTrigger("Die");
                //animatedEnemy.GetComponent<Animator>().SetTrigger("Die");
                enemyDead = true;

                /*foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }*/
            }
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }
}