using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class PlayerController : MonoBehaviour
{
    [Header("Other Player Parameters")]
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public Animator animator;

    [Header("Movement and Speed Parameters")]
    private Vector2 movement;
    private float speed = 12;
    private float jumpForce = 1000;
    private float gravityModifier = 4;

    [Header("Jump and isOnGround Parameters")]
    bool isOnGround = true;
    bool isOnWall = false;
    int jumpTimes = 0;

    [Header("Attack Parameters")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 1;
    [SerializeField] private float damage = 1;
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

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Physics2D.gravity *= gravityModifier;

        if(GameManager.manager.currentLevel != "")
        {
            transform.position = GameObject.Find(GameManager.manager.currentLevel).
                transform.GetChild(0).transform.position;
        }
    }

    void Update()
    {
        Move();

        Jump();

        //Attack();
        Combo();
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

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * speed;

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        movement = new Vector2(horizontal, 0);

        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void Jump()
    {
        if(isOnGround || jumpTimes < 2 || isOnWall)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("IsJumping", true);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isOnGround = false;
                jumpTimes += 1;
                if(jumpTimes > 0)
                {
                    jumpForce = 600;
                }
            }
        }
    }

    public void Combo()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > attackCounter + attackMaxTime)
        {
            attacking = true;
            attackCounter = Time.time;

            if (lastAttackTimer == 0)
            {
                animator.SetTrigger("Attack1");
                lastAttackBool = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(1);
                }
            }
            else
            {
                animator.SetTrigger("Attack2");

                lastAttackTimer = 0;
                lastAttackBool = false;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(2);
                }
            }
        }
        else
        {
            attacking = false;
        }
    }

    void Attack()
    {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > attackCounter + attackMaxTime)
        {
            animator.SetTrigger("Attack1");

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Health>().TakeDamage(damage);
            }

            attacking = true;
            attackCounter = Time.time;
        }
        else
        {
            attacking = false;
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            animator.SetTrigger("Attack2");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("Attack3");
        }
    }

    void Block()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("Block");
            blocking = true;
            blockTimer = blockDuration;
        }

        if(blocking)
        {
            blockTimer -= Time.deltaTime;
            if(blockTimer <= 0f)
            {
                blocking = false;
            }
        }

        Debug.Log(blocking);
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isOnWall = true;
        }
        else
        {
            isOnWall = false;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            animator.SetBool("IsJumping", false);
            jumpTimes = 0;
            jumpForce = 1000;
        }
    }

    void FixedUpdate()
    {
        transform.Translate(movement * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LevelTrigger"))
        {
            GameManager.manager.currentLevel = collision.gameObject.name;
            Physics2D.gravity = new Vector3(0, -9.8F, 0);
            SceneManager.LoadScene(collision.gameObject.GetComponent<LoadLevel>().levelToLoad);
        }
    }
}
