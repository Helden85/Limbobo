using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Rigidbody2D and Animator")]
    Rigidbody2D rb2d;
    [SerializeField] GameObject animatedGhost;

    [Header("Seeing Player Parameters")]
    [SerializeField] GameObject player;
    public float radius = 5;
    [Range(0, 360)] public float angle = 45;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public bool CanSeePlayer; //{ get; set; }

    [Header("Player Hide Fetches")]
    //public bool ifCanSeePlayer = false;
    //bool fetchedIfPlayerIsHiding = false;

    [Header("Speed and Distance Parameters")]
    float speed = 4;
    float walkSpeed = 2;
    float distance = 5;
    Vector2 target;

    [Header("Agro Parameters")]
    bool isAgro = false;
    float maxAgroCounter = 5;
    public float agroCounter = 0;

    [Header("Attack Parameters")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    float fireRate = 1;
    float projectileSpeed = 5;
    float nextFireTime = 0;

    [Header("Patrol Parameters")]
    public GameObject leftPoint;
    public GameObject rightPoint;
    Vector2 leftTarget;
    Vector2 rightTarget;
    public GameObject[] patrolpoints;
    int patrolIndex;
    Vector3 currentPatrolpoint;
    Vector2 lastMoveDirection = Vector2.left;

    [Header("Health and Death Parameters")]
    public Health healthScript;
    bool fetchedDeadBool;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        target = player.transform.position;

        rb2d = GetComponent<Rigidbody2D>();

        currentPatrolpoint = patrolpoints[patrolIndex].transform.position;
    }

    private void Update()
    {
        float shootingDistance = Vector2.Distance(transform.position, player.transform.position);

        if(CanSeePlayer && shootingDistance < 10)
        {
            isAgro = true;
            agroCounter = 0;
        }
        else
        {
            if(isAgro)
            {
                if(agroCounter < maxAgroCounter)
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

        if(isAgro && shootingDistance < 7)
        {
            Attack();
        }
        else if(isAgro)
        {
            Chase();
        }
        else
        {
            Patrol();
        }

        Death();
    }

    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while(true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector2.Angle(transform.right * transform.localScale.x, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if(!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget,
                    obstructionLayer))
                {
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
        else if(CanSeePlayer)
        {
            CanSeePlayer = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector2 angle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        Vector2 angle02 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);

        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(transform.position, angle01 * radius);
        //Gizmos.DrawLine(transform.position, angle02 * radius);

        if(CanSeePlayer)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, player.transform.position);
        }
    }

    Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void Attack()
    {
        if(Time.time >= nextFireTime)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Vector2 fireDirection = ((Vector2)player.transform.position + Vector2.up * 1.5f - (Vector2)transform.position);
            projectile.GetComponent<Rigidbody2D>().velocity = fireDirection * projectileSpeed;

            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    void Chase()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < distance)
        {
            target = player.transform.position;
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            rb2d.velocity = direction * speed;

            if (direction.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
        else if (distanceToPlayer > distance && Vector2.Distance(transform.position, target) < 0.1f)
        {
            target = new Vector2(player.transform.position.x, player.transform.position.y + 2);
        }
        else
        {
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            rb2d.velocity = direction * speed;

            if (direction.x > 0)
            {
                transform.localScale = new Vector2(1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    void StopChasingPlayer()
    {
        isAgro = false;
    }

    void Patrol()
    {
        //anim.SetBool("Moving", true);

        //rb2d.velocity = new Vector2(walkSpeed * gameObject.transform.localScale.x, 0);

        /*if (gameObject.transform.position.x < leftPoint.transform.position.x && gameObject.transform.localScale.x == -1)
        {
            transform.localScale = new Vector2(1, 1);
        }
        if (gameObject.transform.position.x > rightPoint.transform.position.x && gameObject.transform.localScale.x == 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }*/

        transform.position = Vector2.MoveTowards(transform.position, currentPatrolpoint,
            walkSpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, currentPatrolpoint) < 0.1f)
        {
            patrolIndex++;
            if(patrolIndex >= patrolpoints.Length)
            {
                patrolIndex = 0;
            }
            currentPatrolpoint = patrolpoints[patrolIndex].transform.position;
        }

        Vector2 moveDirection = currentPatrolpoint - transform.position;
        if(moveDirection.magnitude > 0.1f)
        {
            moveDirection.Normalize();
            lastMoveDirection = moveDirection;
        }
        transform.localScale = new Vector2(Mathf.Sign(lastMoveDirection.x), 1);

    }

    void Death()
    {
        if (fetchedDeadBool)
        {
            this.enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<CapsuleCollider2D>().enabled = false;

            StartCoroutine(Vanish());
        }
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(5);
        //GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}