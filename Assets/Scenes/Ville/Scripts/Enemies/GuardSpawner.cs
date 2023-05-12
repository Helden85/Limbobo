using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner : MonoBehaviour
{
    [Header("Security Camera Spawn Parameters")]
    [SerializeField] GameObject dataObject;
    bool fetchedBooleanPlayerOnCamera;

    public GameObject guard1;
    public GameObject guard2;

    private void Update()
    {
        fetchedBooleanPlayerOnCamera = dataObject.GetComponent<SecurityCamera>().playerOnCamera;

        if(fetchedBooleanPlayerOnCamera)
        {
            guard1.SetActive(true);
            guard2.SetActive(true);
        }
    }
}
