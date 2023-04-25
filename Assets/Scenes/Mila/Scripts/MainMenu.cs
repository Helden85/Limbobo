using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject optionsMenu;
    void Start()
    {
        
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
}
