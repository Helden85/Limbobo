using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class NavigateWithController : MonoBehaviour
{
    [SerializeField] int scene;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        StartGameWithController();



    }

    public void StartGameWithController()
    {
         if (Input.GetKey(KeyCode.Joystick1Button1))
        {
            SceneManager.LoadScene(scene);
          
        }

    }
}
