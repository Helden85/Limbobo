using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject optionsMenu;
    public GameObject controls;
    public GameObject pcControls;
    public GameObject psControls;
    [SerializeField] AudioSource clickSound;

    void Start()
    {
        //ControlsForController();

    }


    void Update()
    {
        

    }
    public void OptionButton()
    {
        startScreen.SetActive(false);
        optionsMenu.SetActive(true);

    }
    public void ExitToMenu()
    {
        startScreen.SetActive(true);
        optionsMenu.SetActive(false);

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
        if (Gamepad.current.leftStick.IsActuated())
        {
            pcControls.SetActive(false);
            psControls.SetActive(true);

        }
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
