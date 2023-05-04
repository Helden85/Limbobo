using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerController : MonoBehaviour
{
    [Header("Other Player Parameters")]
    private Rigidbody2D rb2d;
    public GameObject animatedPlayer;
    private CapsuleCollider2D capsuleCollider;

    [Header("Speed and Jump Parameters")]
    float moveSpeed = -10;
    float jumpForce = 15;
    float jumpTimes = 0;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    public float horizontalInput { get; set; }

    [Header("Hiding Parameters")]
    bool hiding = false;
    int originalLayer;
    public float hidingDistance = 2f;
    private List<GameObject> hidingPlaces;
    private GameObject[] enemies;
    bool fetchedIfEnemyCanSeePlayer = false;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animatedPlayer.GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        originalLayer = gameObject.layer;
        hidingPlaces = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hide"));
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void Update()
    {
        HidingMechanics();

        if (!hiding)
        {
            Move();
            Jump();
        }
    }

    void HidingMechanics()
    {
        foreach (GameObject enemy in enemies)
        {
            fetchedIfEnemyCanSeePlayer = enemy.GetComponent<GuardPlat>().ifCanSeePlayer;
        }

        if (!hiding && !fetchedIfEnemyCanSeePlayer)
        {
            float closestDistance = Mathf.Infinity;
            GameObject closestHidingPlace = null;
            foreach (GameObject hidePlace in hidingPlaces)
            {
                float distanceToHidingPlace = Vector2.Distance(transform.position, hidePlace.transform.position);
                if (distanceToHidingPlace < closestDistance)
                {
                    closestDistance = distanceToHidingPlace;
                    closestHidingPlace = hidePlace;
                }
            }

            if (closestDistance < hidingDistance && Input.GetKeyDown(KeyCode.E) && !hiding)
            {
                animatedPlayer.SetActive(false);
                gameObject.layer = LayerMask.NameToLayer("Hidden");
                hiding = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && hiding)
        {
            animatedPlayer.SetActive(true);
            gameObject.layer = originalLayer;
            hiding = false;
        }
    }

    public void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
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
}
