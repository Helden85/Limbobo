using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject controls;
    public GameObject pauseMenu;
    public GameObject psControls;
     [SerializeField] AudioSource clickSound;
   

    void Start()
    {
        ControlsForController();

    }

        void Update()
    {
        

    }
     public void ControlsButton()
    {
         controls.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void ExitToOptions()
    {
        optionsMenu.SetActive(true);
        controls.SetActive(false);
    }

 

    public void ControlsForController()
    {
        /*if (Gamepad.current.leftStick.IsActuated())
        {
            controls.SetActive(false);
            psControls.SetActive(true);

        }*/
    }
     public void OptionsButton()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);

    }
    public void ExitToPauseMenu()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
       public void ExitToOptionsFromPsControls()
    {
        optionsMenu.SetActive(true);
        psControls.SetActive(false);
    }
    public void PlayClickSound()
    {
        clickSound.Play();
    }

    public void SetDefaultButton(Button newDefaultButton)
    {
    EventSystem.current.SetSelectedGameObject(newDefaultButton.gameObject);
    }
}
    


