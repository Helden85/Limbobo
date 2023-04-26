using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer2Animations : MonoBehaviour
{
    Animator anim;
    public GameObject playerScript;
    public float fetchedHorizontalInput { get; set; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        fetchedHorizontalInput = playerScript.GetComponent<PlayerController>().horizontalInput;
    }

    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(fetchedHorizontalInput));
    }
}
