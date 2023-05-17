using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEffect : MonoBehaviour
{
    [SerializeField] Animator animator;
    public float interval = 30f;
    private float timer;
    
    void Start()
    {
        
         timer = interval;
        
    }

    
    void Update()
    {
        timer -= Time.deltaTime; 

        if (timer <= 0f)
        {
            PlayAnimation(); 
            timer = interval; 
        }
   
    }

 public void PlayAnimation()
    {
        animator.SetTrigger("PlayAnimation"); 
    }
}
