using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MovingPlatform : MonoBehaviour
{
    //public Transform pointA, pointB;
    Vector2 pointA, pointB;

    public float moveFreq = 2;
    public float moveDistance;

    public bool isHorizontal;



    private void Start()
    {
        pointA = transform.position;
    }

    private void Update()
    {
        if(isHorizontal)
        {
            pointB.x = pointA.x + Mathf.Sin(Time.time * moveFreq) * moveDistance;

            transform.position = new Vector2(pointB.x, transform.position.y);
        }
        else
        {
            pointB.y = pointA.y + Mathf.Sin(Time.time * moveFreq) * moveDistance;

            transform.position = new Vector2(transform.position.x, pointB.y);
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
