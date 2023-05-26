using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    GameObject player;
    public bool playerDead = false;
    bool playerBlock;

    [Header("Enemy Fetch Animations")]
    public GameObject animatedEnemy;
    [SerializeField] AudioSource hitPlayerSound;
    public float amountFirstAid;
    public TMP_Text firstAidText;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }*/

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerBlock = player.GetComponent<Combat>().blocking;
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

        if (gameObject.CompareTag("Player") && currentHealth > 0 && playerBlock)
        {

        }
        else if (gameObject.CompareTag("Player") && currentHealth > 0)
        {
            animatedPlayer.GetComponent<Animator>().SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
        }
        else if (gameObject.CompareTag("Player"))
        {
            if (!dead)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Die");
                //GetComponent<Animator>().SetTrigger("Die");
                //GetComponent<FreeAssetPlayerController>().enabled = false;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<Combat>().enabled = false;
                //GetComponent<PlayerController>().enabled = false;

                /*foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }*/
                playerDead = true;
                dead = true;
                hitPlayerSound.enabled = false;


            }
        }

        if (gameObject.CompareTag("Enemy") && currentHealth > 0)
        {
            //anim.SetTrigger("Hurt");
            animatedEnemy.GetComponent<Animator>().SetTrigger("Hurt");
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            if (!dead)
            {
                //anim.SetTrigger("Die");
                animatedEnemy.GetComponent<Animator>().SetTrigger("Die");
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

    public void Respawn()
    {

        hitPlayerSound.enabled = true;
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("Die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        /*foreach (Behaviour component in components)
        {
            component.enabled = true;
        }*/

        //GetComponent<FreeAssetPlayerController>().enabled = true;
        GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 12, true);
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 12, false);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    public void CoinCollected()
    {
        amountFirstAid++;
        firstAidText.text = amountFirstAid.ToString();



    }

}