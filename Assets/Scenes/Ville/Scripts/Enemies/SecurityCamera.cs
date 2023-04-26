using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public Transform castPoint;

    private float rotateSpeed = 60;
    private float distance = 10;

    [SerializeField] Transform player;

    private Rigidbody2D rb2d;

    public bool playerOnCamera = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hitCamera = Physics2D.Raycast(transform.position, transform.right, distance,
            1 << LayerMask.NameToLayer("Action"));

        if (hitCamera.collider != null)
        {
            if (hitCamera.collider.gameObject.CompareTag("Player"))
            {
                playerOnCamera = true;
                Debug.DrawLine(transform.position, hitCamera.point, Color.red);
            }
            else
            {
                playerOnCamera = false;
                Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.yellow);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.blue);
        }

        transform.localEulerAngles = new Vector3(0, 0, Mathf.PingPong(Time.time * rotateSpeed,
        140) - 160);
    }
}
