using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitCredits : MonoBehaviour
{
     [SerializeField] int scene;
   
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.JoystickButton2))
        {
            SceneManager.LoadScene(scene);
            
        }
    }
}
