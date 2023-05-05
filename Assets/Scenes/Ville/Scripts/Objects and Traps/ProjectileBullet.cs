using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    float maxDistance = 40;
    float distanceTravelled = 0;
    Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        distanceTravelled += Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;

        if(distanceTravelled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}
