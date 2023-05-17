using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] Button newDefaultButton;
    [SerializeField] AudioSource GameOverSound;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(newDefaultButton.gameObject); //This sets the default button so a controller can be used
        //SoundManager.instance.PlaySound(gameOverSound);
        GameOverSound.Play(); //Not sure how the method above works
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
    //  public void SetDefaultButton(Button newDefaultButton)
    // {
    // EventSystem.current.SetSelectedGameObject(newDefaultButton.gameObject);
    // }
}
