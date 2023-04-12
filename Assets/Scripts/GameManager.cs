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
            // Jos ei ole manageria, kerrottaan että tämä luokka on se manageri
            // Kerrotaan myös, että tämä manageri ei saa tuhoutua jos scene vaihtuu toiseen
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else
        {
            // Tämä ajetaan silloin jos on jo olemassa manageri ja ollaan luomasa toinen manageri joka on liikaa!
            // Tällöin tämä manageri tuhotaan pois, jolloin jää vain se ensimmäinen.
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
