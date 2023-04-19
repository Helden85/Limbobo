using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float lineWidth = 0.1f;

    private LineRenderer line;
    private BoxCollider2D boxCollider;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        transform.localScale = new Vector2(-1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(4);
        }
    }

    private void Update()
    {
        line.positionCount = 2;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;

        boxCollider.size = new Vector2(Vector2.Distance(start.position, end.position), lineWidth);
        boxCollider.offset = new Vector2(boxCollider.size.x / 2, 0);

        line.SetPosition(0, start.position);
        line.SetPosition(1, end.position);
    }
}
