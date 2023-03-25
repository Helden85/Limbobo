using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

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
}
