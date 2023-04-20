using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCombat : MonoBehaviour
{
    [Header("Other Player Parameters")]
    public Animator anim;

    // Variables for moving 
    [SerializeField] float speed = 1f;
    [SerializeField] float runSpeed;
    float movement;
    float waitbySecs;


    //Variables for jumping mechanic
    [SerializeField] int maxJumps = 2;
    int jumps;
    [SerializeField] float jumpForce = 5f;
    public bool isGrounded;
    //Invisible walls
    [SerializeField] float xRangeLeft;
    [SerializeField] float xRangeRight;
    //[SerializeField] AudioSource impactSound;

    [Header("Attack Parameters")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    //[SerializeField] private float damage = 1;
    float attackMaxTime = 0.25f;
    float attackCounter = 0;
    public bool attacking = false;

    [Header("Blocking Parameters")]
    public bool blocking = false;
    float blockDuration = 0.5f;
    public float blockTimer;

    [Header("Combo Parameters")]
    float lastAttackMaxTime = 0.5f;
    public float lastAttackTimer = 0;
    bool lastAttackBool = false;
    public bool attackTwo = false;

    public void Update()
    {
        PlayerControls();
        PlayerBoundaries();

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
            }
        }

        Block();
    }

    //Allows the player to jump twice
    public void Jump()
    {
        if (jumps > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }


    public void PlayerControls()
    {

        // Allows the player to move left and right using arrows and AD
        movement = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(movement));

        transform.Translate(Vector2.left * Time.deltaTime * speed * movement);

        //Turns player to look in the moving direction
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector2.left * Time.deltaTime * runSpeed * movement);

        }

        //Player can jump by using space

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Jump();
        }

    }

    public void PlayerBoundaries()
    {
        //Invisible walls on the horizontal axis

        if (transform.position.x < xRangeLeft)
        {
            transform.position = new Vector2(xRangeLeft, transform.position.y);

        }
        if (transform.position.x > xRangeRight)
        {
            transform.position = new Vector2(xRangeRight, transform.position.y);

        }

    }


    //Checks if the player is touching the ground
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            jumps = maxJumps;
            isGrounded = true;
            //impactSound.Play();

        }
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
                anim.SetTrigger("Attack1");
                lastAttackBool = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(1);
                }
            }
            else
            {
                anim.SetTrigger("Attack2");

                lastAttackTimer = 0;
                lastAttackBool = false;
                attackTwo = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(2);
                }
            }
        }
        else
        {
            attacking = false;
            attackTwo = false;
        }
    }

    public void Block()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("Block");
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