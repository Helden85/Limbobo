using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
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
    public bool playerDead;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
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

        if(gameObject.CompareTag("Player") && currentHealth > 0)
        {
            animatedPlayer.GetComponent<Animator>().SetTrigger("Hurt");
        }
        else if(gameObject.CompareTag("Player"))
        {
            if (!dead)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Die");
                //enemyDead = true;
                //GetComponent<PlayerMovement>().enabled = false;
                //GetComponent<PlayerController>().enabled = false;

                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                dead = true;
            }
        }

        if (gameObject.CompareTag("Enemy") && currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
        }
        else if(gameObject.CompareTag("Enemy"))
        {
            if(!dead)
            {
                anim.SetTrigger("Die");
                enemyDead = true;
                //GetComponent<PlayerMovement>().enabled = false;
                //GetComponent<PlayerController>().enabled = false;

                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }

                dead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
