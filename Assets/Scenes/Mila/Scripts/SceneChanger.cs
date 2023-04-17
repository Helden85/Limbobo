using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] int scene;
    
    void Start()
    {

    }

   
    void Update()
    {

    }

    //Changes the scene by scene number
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    //Changes the scene on collision according to the tag
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider"))
        {
            SceneManager.LoadScene(scene);
        }
    }

}
