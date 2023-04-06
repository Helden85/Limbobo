using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform castPoint;
    private float distance = 30;
    [SerializeField] Transform player;
    private Rigidbody2D rb2d;
    public bool playerOnCamera = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        Vector2 endPos = castPoint.position + Vector3.right * distance * transform.localScale.x;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPos, 1 << LayerMask.NameToLayer("Action"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                playerOnCamera = true;
                Debug.DrawLine(castPoint.position, hit.point, Color.red);
            }
            else
            {
                playerOnCamera = false;
                Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
            }
        }
        else
        {
            Debug.DrawLine(castPoint.position, endPos, Color.blue);
        }
    }
}
