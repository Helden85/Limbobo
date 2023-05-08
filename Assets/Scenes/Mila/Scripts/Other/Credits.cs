using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Credits : MonoBehaviour
{
    //Variables
    [SerializeField] float speed = 2.0f;
    [SerializeField] float creditsDisappear;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject thankYou;
   
    void Update()
    {
        
            if (transform.position.y < creditsDisappear)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
              
            }
            else if (transform.position.y > creditsDisappear)
            {

                credits.SetActive(false);
                thankYou.SetActive(true);
                
            }
    }
}
