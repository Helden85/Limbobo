using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject animatedPlayer;

    [Header("Attack Parameters")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    float attackMaxTime = 0.25f;
    float attackCounter = 0;
    public bool attacking = false;

    [Header("Blocking Parameters")]
    public bool blocking = false;
    float blockDuration = 0.5f;
    public float blockTimer;

    [Header("Combo Parameters")]
    float lastAttackMaxTime = 1.2f;
    public float lastAttackTimer = 0;
    public bool lastAttackBool;
    public bool attackTwo;
    public bool attackThree;

    [Header("PlayerMovement Fetches")]
    bool onGround;
    bool moving;

    [SerializeField] AudioSource attack1Sound;
    [SerializeField] AudioSource attack2Sound;
    [SerializeField] AudioSource attack3Sound;
    [SerializeField] AudioSource hitEnemySound;

    void Start()
    {
        animatedPlayer.GetComponent<Animator>();
    }

    void Update()
    {
        onGround = GetComponent<PlayerMovement>().isGrounded;
        moving = GetComponent<PlayerMovement>().isMoving;

        if (onGround && !moving)
        {
            Attack();
            Block();
        }

        if (lastAttackBool)
        {
            if (lastAttackTimer < lastAttackMaxTime)
            {
                lastAttackTimer += Time.deltaTime;
            }
            else
            {
                lastAttackTimer = 0;
                lastAttackBool = false;
                attackTwo = false;
                attackThree = false;
            }
        }
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

       
        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > attackCounter + attackMaxTime 
        || Input.GetKeyDown(KeyCode.JoystickButton7) && Time.time > attackCounter + attackMaxTime)
        {
            attacking = true;
            attackCounter = Time.time;

            if (lastAttackTimer == 0)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Attack1");
                lastAttackBool = true;
                attack1Sound.Play();

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(1);
                    hitEnemySound.Play();
                }
            }
            else if (attackTwo == false)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Attack2");
                attackTwo = true;
                attack2Sound.Play();

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(2);
                    hitEnemySound.Play();
                }
            }
            else if (attackTwo == true && attackThree == false)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Attack3");
                attackThree = true;
                attack3Sound.Play();

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(3);
                    hitEnemySound.Play();
                }
            }
        }

        else
        {
            attacking = false;
        }
    }

    public void Block()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            animatedPlayer.GetComponent<Animator>().SetTrigger("Block");
            blocking = true;
            blockTimer = blockDuration;
        }

        if (blocking)
        {
            blockTimer -= Time.deltaTime;
            if (blockTimer <= 0f)
            {
                blocking = false;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
