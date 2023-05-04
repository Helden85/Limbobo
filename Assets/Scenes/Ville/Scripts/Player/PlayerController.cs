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
    GameObject hidingPlace;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animatedPlayer.GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        originalLayer = gameObject.layer;
        hidingPlace = GameObject.FindGameObjectWithTag("Hide");
    }

    public void Update()
    {
        //Move();
        Jump();

        float distanceToHidingPlace = Vector2.Distance(transform.position, hidingPlace.transform.position);

        if(distanceToHidingPlace < hidingDistance && Input.GetKeyDown(KeyCode.E) && !hiding)
        {
            animatedPlayer.SetActive(false);
            gameObject.layer = LayerMask.NameToLayer("Hidden");
            hiding = true;
        }
        else if(Input.GetKeyDown(KeyCode.E) && hiding)
        {
            animatedPlayer.SetActive(true);
            gameObject.layer = originalLayer;
            hiding = false;
        }

        if(!hiding)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Box"))
        {
            //canHide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Box"))
        {
            //canHide = false;
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
