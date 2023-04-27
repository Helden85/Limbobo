using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeerController : MonoBehaviour
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

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    public void Move()
    {
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;
        //anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
        rb2d.velocity = new Vector2(horizontalInput, rb2d.velocity.y);

        if (horizontalInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
