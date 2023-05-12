using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNoPatrol : MonoBehaviour
{
    [Header("Seeing and Player Transform Parameters")]
    [SerializeField] Transform castPoint;
    [SerializeField] Transform player;

    [Header("Speed Parameters")]
    [SerializeField] float moveSpeed;

    [Header("Rigidbody2D and Animator")]
    Rigidbody2D rb2d;
    public Animator anim;

    [Header("Guard Parameters")]
    public GameObject guardPoint;
    float guardMaxTime = 5;
    public float guardTimer;
    bool directionRight = false;

    [Header("Agro Parameters")]
    [SerializeField] float agroRange;
    bool isAgro = false;
    float maxAgroCounter = 5;
    public float agroCounter = 0;

    [Header("Attack Distance and Timing Parameters")]
    public float attackDistance = 2.4f;
    private float maxTimeBetweenAttacks = 0.1f;
    public float attackCounter = 0;

    [Header("Attack Parameters")]
    [SerializeField] private float damage = 1;
    [SerializeField] private CapsuleCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    private Health playerHealth;
    bool playerBlock;
    bool playerAttackBool;

    [Header("Health and Death Parameters")]
    public Health healthScript;
    bool fetchedDeadBool;

    [Header("Security Camera Parameters")]
    [SerializeField] GameObject dataObject;
    bool fetchedBooleanPlayerOnCamera;

    [Header("OnCollision Parameters")]
    private float push = 2;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        fetchedBooleanPlayerOnCamera = dataObject.GetComponent<SecurityCamera>().playerOnCamera;
        fetchedDeadBool = healthScript.GetComponent<Health>().enemyDead;
        //fetchedIfPlayerIsHiding = player.GetComponent<PlayerController>().hiding;
        //playerAttackBool = player.GetComponent<Combat>().lastAttackBool;
        playerBlock = player.GetComponent<Combat>().blocking;


        if (CanSeePlayer(agroRange) || fetchedBooleanPlayerOnCamera || playerAttackBool && distToPlayer < 10)
        {
            isAgro = true;
            agroCounter = 0;
        }
        else
        {
            if (isAgro)
            {
                if (agroCounter < maxAgroCounter)
                {
                    agroCounter += Time.deltaTime;
                }
                else
                {
                    agroCounter = 0;
                    StopChasingPlayer();
                }
            }
        }

        if (isAgro && distToPlayer < attackDistance)
        {
            //anim.SetBool("Moving", false);
            if (Time.time > attackCounter + maxTimeBetweenAttacks)
            {
                AttackPlayer();
                attackCounter = Time.time;
            }
        }
        else if (isAgro)
        {
            ChasePlayer();
        }
        /*else
        {
            Guard();
        }*/

        Death();
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;

        Vector2 endPos = castPoint.position + Vector3.right * distance * transform.localScale.x;

        RaycastHit2D hitLine = Physics2D.Linecast(castPoint.position, endPos,
            1 << LayerMask.NameToLayer("Action"));

        if (hitLine.collider != null)
        {
            if (hitLine.collider.gameObject.CompareTag("Player"))
            {
                val = true;
                Debug.DrawLine(castPoint.position, hitLine.point, Color.red);
            }
            else
            {
                val = false;
                Debug.DrawLine(castPoint.position, hitLine.point, Color.yellow);
            }
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }

        return val;
    }

    private bool AttackPlayer()
    {

        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
        0, Vector2.left, 0, playerLayer);


        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                //anim.SetTrigger("Attack");
                playerHealth = hit.transform.GetComponent<Health>();
            }
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (AttackPlayer())
        {
            if (playerBlock)
            {
                playerHealth.TakeDamage(0);
            }
            else
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void ChasePlayer()
    {
        //anim.SetBool("Moving", true);

        if (transform.position.x < player.position.x)
        {
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void StopChasingPlayer()
    {
        isAgro = false;
    }

    void Guard()
    {
        float distToGuardPoint = Vector2.Distance(transform.position, guardPoint.transform.position);

        if (distToGuardPoint < 0.2f)
        {
            //anim.SetBool("Moving", false);

            if (guardTimer <= guardMaxTime)
            {
                guardTimer += Time.deltaTime;
            }
            else
            {
                if (directionRight)
                {
                    transform.localScale = new Vector2(1, 1);
                    directionRight = false;
                }
                else
                {
                    transform.localScale = new Vector2(-1, 1);
                    directionRight = true;
                }

                guardTimer = 0;
            }
        }
        else
        {
            //anim.SetBool("Moving", true);

            rb2d.velocity = new Vector2(moveSpeed * gameObject.transform.localScale.x, 0);

            if (gameObject.transform.position.x < guardPoint.transform.position.x && gameObject.transform.localScale.x == -1)
            {
                transform.localScale = new Vector2(1, 1);
                directionRight = false;
            }
            if (gameObject.transform.position.x > guardPoint.transform.position.x && gameObject.transform.localScale.x == 1)
            {
                transform.localScale = new Vector2(-1, 1);
                directionRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAgro = true;
            agroCounter = 0;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(push * Vector3.left, ForceMode2D.Impulse);
        }
    }

    void Death()
    {
        if (fetchedDeadBool)
        {
            this.enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
