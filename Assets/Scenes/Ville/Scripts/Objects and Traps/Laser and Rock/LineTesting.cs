using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;

    void Start()
    {
        line.SetUpLine(points);
    }

    private void Update()
    {
        
    }
}
