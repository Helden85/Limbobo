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

    void Start()
    {
        animatedPlayer.GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
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

        Block();

        Debug.Log("Blocking " + blocking);
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > attackCounter + attackMaxTime)
        {
            attacking = true;
            attackCounter = Time.time;

            if (lastAttackTimer == 0)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Attack1");
                lastAttackBool = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(1);
                }
            }
            else if (attackTwo == false)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Attack2");
                attackTwo = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(2);
                }
            }
            else if (attackTwo == true && attackThree == false)
            {
                animatedPlayer.GetComponent<Animator>().SetTrigger("Attack3");
                attackThree = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(3);
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
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

        Debug.Log(blocking);
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
