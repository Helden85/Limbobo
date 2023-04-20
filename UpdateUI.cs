using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] private GameObject ObjectPrefab;
    private TextMeshProUGUI UIText;
    private string ObjectID;

    private void Awake()
    {
        UIText = GetComponent<TextMeshProUGUI>();
        ObjectID = ObjectPrefab.GetComponent<Object>().ID;
    }

    private void LateUpdate()
    {
        UIText.text = PlayerPrefs.GetInt(ObjectID).ToString();
    }
}
