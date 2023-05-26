using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;

    // Variables for moving 
    [SerializeField] float speed = 1f;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float currentSpeed;
    float movement;

    //Variables for jumping mechanic
    [SerializeField] int maxJumps = 2;
    int jumps;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float wallJumpForce;
    public bool isGrounded;
    public bool isCrouched;
    bool canRunFunctionNow = false;

    //Invisible walls
    [SerializeField] float xRangeLeft;
    [SerializeField] float xRangeRight;
    [SerializeField] float yRangeUp;

    //Audio
    [SerializeField] AudioSource impactSound;

    Animator anim;

    [Header("Hiding Parameters")]
    [SerializeField] GameObject animatedPlayer;
    public bool hiding = false;
    int originalLayer;
    public float hidingDistance = 2f;
    private List<GameObject> hidingPlaces;
    private GameObject[] enemies;
    bool fetchedIfEnemyCanSeePlayer = false;
    public Transform playerTransform;
    Vector2 currentVelocity;

    public bool isMoving;
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private LayerMask wallLayer;
    public float coinsCollected;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Vector2 currentVelocity = rb2d.velocity;
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        originalLayer = gameObject.layer;
        hidingPlaces = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hide"));
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    void Awake()
    {
        PlayerPrefs.DeleteAll();
    }

    void Update()
    {
        HidingMechanics();

        if (!hiding)
        {
            PlayerControls();
        }

        PlayerBoundaries();
        StartCoroutine(DisableImpactSound());

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GetComponent<Health>().amountFirstAid > 0)
            {
                GetComponent<Health>().AddHealth(1);
                GetComponent<Health>().amountFirstAid--;
                GetComponent<Health>().firstAidText.text = GetComponent<Health>().amountFirstAid.ToString();
            }

        }


    }

    public void HidingMechanics()
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

            if (closestDistance < hidingDistance && Input.GetKeyDown(KeyCode.E) && !hiding
            || closestDistance < hidingDistance && Input.GetKeyDown(KeyCode.JoystickButton3) && !hiding)
            {
                animatedPlayer.SetActive(false);
                gameObject.layer = LayerMask.NameToLayer("Hidden");
                hiding = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && hiding || Input.GetKeyDown(KeyCode.JoystickButton3) && hiding)
        {
            animatedPlayer.SetActive(true);
            gameObject.layer = originalLayer;
            hiding = false;
        }
    }

    //Allows the player to jump twice
    public void Jump()
    {
        if (jumps > 0)
        {

            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
        else if (onWall())
        {
            rb2d.AddForce(new Vector2(0, wallJumpForce), ForceMode2D.Impulse);
        }
    }

    public void PlayerControls()
    {
        // Allows the player to move left and right using arrows and AD or the left joystick
        movement = Input.GetAxis("Horizontal");

        isMoving = Mathf.Abs(movement) > 0.5f;

        //Turns player to look in the moving direction
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);

        }

        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);

        }

        //Player can run by holding down shift or L1 button on playstation controller

        //if (isGrounded == true && Input.GetKey(KeyCode.RightShift) ||isGrounded == true &&  Input.GetKey(KeyCode.LeftShift) || isGrounded == true &&  Input.GetKey(KeyCode.JoystickButton4))
        // {
        //     transform.Translate(Vector2.left * Time.deltaTime * runSpeed * movement);


        // }
        //  if (isGrounded == true && Input.GetKey(KeyCode.U) || isGrounded == true && Input.GetKey(KeyCode.JoystickButton0))
        // {

        //     transform.Translate(Vector2.left * Time.deltaTime * crouchSpeed * movement);


        // }
        // else
        // {
        //     transform.Translate(Vector2.left * Time.deltaTime * speed * movement);


        // }

        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton4))
        {
            currentSpeed = runSpeed;

        }
        else if (isGrounded == true && Input.GetKey(KeyCode.U) || isGrounded == true && Input.GetKey(KeyCode.JoystickButton0))
        {
            currentSpeed = crouchSpeed;

        }
        else
        {
            currentSpeed = speed;

        }
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed * movement);

        //Player can jump by using either space or cross on ps4/5 controller

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {

            this.Jump();

        }

    }
    bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center,
            capsuleCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
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
        if (transform.position.y > yRangeUp)
        {
            transform.position = new Vector2(transform.position.x, yRangeUp);
        }

    }
    IEnumerator DisableImpactSound()
    {
        canRunFunctionNow = false;
        yield return new WaitForSeconds(1.0f);
        canRunFunctionNow = true;
        impactSound.enabled = true;
    }


    //Checks if the player is touching the ground
    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            jumps = maxJumps;
            isGrounded = true;
            impactSound.Play();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coinsCollected++;
            if (coinsCollected > 2)
            {

                GetComponent<Health>().CoinCollected();
                coinsCollected = 0;
            }
        }
    }


}
