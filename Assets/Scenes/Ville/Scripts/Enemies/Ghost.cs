using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    Rigidbody2D rb2d;

    [SerializeField] GameObject player;
    public float radius = 5;
    [Range(0, 360)] public float angle = 45;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;
    public bool CanSeePlayer; //{ get; set; }

    float speed = 4;
    float attackDistance = 5;
    Vector2 target;

    [Header("Agro Parameters")]
    bool isAgro = false;
    float maxAgroCounter = 5;
    public float agroCounter = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());

        target = player.transform.position;

        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(CanSeePlayer)
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
                    rb2d.velocity = Vector2.zero;
                }
            }
        }

        if(isAgro)
        {
            ChaseAndAttack();
        }
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

    void ChaseAndAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < attackDistance)
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
        else if (distanceToPlayer > attackDistance && Vector2.Distance(transform.position, target) < 0.1f)
        {
            target = new Vector2(player.transform.position.x, player.transform.position.y);
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
}