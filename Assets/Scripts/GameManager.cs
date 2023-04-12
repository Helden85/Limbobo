using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public string currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        // singleton
        // Tsekataan, onko manageria jo olemassa
        if(manager == null)
        {
            // Jos ei ole manageria, kerrottaan ett� t�m� luokka on se manageri
            // Kerrotaan my�s, ett� t�m� manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            // T�m� ajetaan silloin jos on jo olemassa manageri ja ollaan luomasa toinen manageri joka on liikaa!
            // T�ll�in t�m� manageri tuhotaan pois, jolloin j�� vain se ensimm�inen.
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
