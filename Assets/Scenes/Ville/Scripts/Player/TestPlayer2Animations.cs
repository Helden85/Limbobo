using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer2Animations : MonoBehaviour
{
    Animator anim;
    public GameObject playerScript;
    public GameObject combatScript;
    public float fetchedHorizontalInput { get; set; }

    //public bool fetchedFirstAttack;
    //public bool fetchedSecondAttack;

    private void Start()
    {
        anim = GetComponent<Animator>();
        //fetchedHorizontalInput = playerScript.GetComponent<PlayerController>().horizontalInput;
        //fetchedFirstAttack = combatScript.GetComponent<Combat>().lastAttackBool;
        //fetchedSecondAttack = combatScript.GetComponent<Combat>().attackTwo;
    }

    private void Update()
    {
        //anim.SetFloat("Speed", Mathf.Abs(fetchedHorizontalInput));

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        /*if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if(!fetchedFirstAttack)
            {
                anim.SetTrigger("Attack1");
            }
            else if(fetchedSecondAttack)
            {
                anim.SetTrigger("Attack2");
            }
        }*/
    }
}
