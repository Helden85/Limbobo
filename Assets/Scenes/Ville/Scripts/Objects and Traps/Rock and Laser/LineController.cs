using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;
    private Transform[] points;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    void Update()
    {
        for(int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }
}
