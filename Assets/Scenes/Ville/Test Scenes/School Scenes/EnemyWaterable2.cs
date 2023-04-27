using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaterable2 : MonoBehaviour
{
    public void EnemyDisappear()
    {
        gameObject.SetActive(false);
    }

    public void WaterHit()
    {
        EnemyDisappear();
    }
}
