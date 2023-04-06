using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealtBar;

    void Start()
    {
        currenthealtBar.fillAmount = playerHealth.currentHealth / 10;
    }

    void Update()
    {
        currenthealtBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
