using System.Collections;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [Header("Rigidbody2D and Speed Parameters")]
    Rigidbody2D rb2d;
    float walkSpeed = 3;
    float runSpeed = 6;

    [Header("Seeing Player Parameters")]
    [SerializeField] GameObject player;
    public float radius = 5;
    [Range(0, 360)] public float angle = 45;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public bool CanSeePlayer; //{ get; set; }

    [Header("Agro Parameters")]
    bool isAgro = false;
    float maxAgroCounter = 5;
    public float agroCounter = 0;

    [Header("Attack Parameters")]
    float attackDistance = 3f;
    float distToPlayer = 1;

    [Header("Fetcing Animator")]
    public GameObject vihollisAnimaatio;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
        rb2d = GetComponent<Rigidbody2D>();
        vihollisAnimaatio.GetComponent<Animator>();
    }

    private void Update()
    {
        distToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (CanSeePlayer)
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

            if (isAgro)
            {
                if (distToPlayer <= attackDistance)
                {
                    AttackPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            }
        }
    }

    void ChasePlayer()
    {
        //anim.SetBool("Moving", true);

        if (transform.position.x < player.transform.position.x)
        {
            rb2d.velocity = new Vector2(runSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-runSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void StopChasingPlayer()
    {
        isAgro = false;
    }

    void AttackPlayer()
    {
        vihollisAnimaatio.GetComponent<Animator>().SetTrigger("Attack");
    }

    IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    void FieldOfViewCheck()
    {
        Collider2D[] rangeChecks = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right * transform.localScale.x, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget,
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
        else if (CanSeePlayer)
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

        if (CanSeePlayer)
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
}