using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Other Player Parameters")]
    private Rigidbody2D rb2d;
    //[SerializeField] Animator anim;
    private CapsuleCollider2D capsuleCollider;

    [Header("Speed and Jump Parameters")]
    float moveSpeed = -10;
    float jumpForce = 15;
    float jumpTimes = 0;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public float horizontalInput { get; set; }

    [Header("Fetch Parameters")]
    Combat combatScript;
    public Animator anim;

    /*[Header("Attack Parameters")]
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
    bool lastAttackBool = false;
    public bool attackTwo = false;
    public bool attackThree = false;*/

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //anim.GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        //combatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
        //combatScript.Attack();
    }

    public void Update()
    {
        Move();
        Jump();

        /*
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

        Block();*/

        //Debug.Log("is on wall " + onWall());
        //Debug.Log("is on ground " + isGrounded());
    }

    public void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        //anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        rb2d.velocity = new Vector2(horizontalInput, rb2d.velocity.y);

        if(horizontalInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if(horizontalInput < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded() || jumpTimes < 1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimes += 1;
            }
            else if (onWall() || jumpTimes < 1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimes += 1;
            }
        }


        if (isGrounded())
        {
            jumpTimes = 0;
        }

        //anim.SetBool("Grounded", isGrounded());
    }

    bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center,
            capsuleCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center,
            capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    /*public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        if (Input.GetKeyDown(KeyCode.RightControl) && Time.time > attackCounter + attackMaxTime)
        {
            attacking = true;
            attackCounter = Time.time;

            if (lastAttackTimer == 0)
            {
                //anim.SetTrigger("Attack1");
                lastAttackBool = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(1);
                }
            }
            else if(attackTwo == false)
            {
                //anim.SetTrigger("Attack2");
                attackTwo = true;

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<Health>().TakeDamage(2);
                }
            }
            else if(attackTwo == true && attackThree == false)
            {
                //anim.SetTrigger("Attack3");
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
            //anim.SetTrigger("Block");
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
    }*/
}
