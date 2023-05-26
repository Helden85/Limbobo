using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowHealthWarning : MonoBehaviour
{
    public GameObject Health;
    private Health healthScript;
    Animator animation;

    void Start()
    {
        animation = GetComponent<Animator>();
        healthScript = Health.GetComponent<Health>();
    }

  
    void Update()
    {
        if (healthScript.currentHealth < 2)
        {
            animation.SetBool("Light", true);

        }
        else if (healthScript.currentHealth > 1)
        {
            animation.SetBool("Light", false);

        }
    }
}
