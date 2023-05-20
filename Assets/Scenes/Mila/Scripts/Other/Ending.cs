using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [SerializeField] int scene;
    [SerializeField] AudioSource monsterSound;
    [SerializeField] GameObject ending;
    bool disableMusicNow;
    void Start()
    {
        
    }

        void Update()
    {
       if (disableMusicNow == true)
       {
            StopMusic();
        }
        
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlayMonsterSound());
            
        }
    }
     IEnumerator PlayMonsterSound()
    {
        disableMusicNow = true;
        ending.SetActive(true);
        monsterSound.Play();
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(scene);
        
    }
public void StopMusic()
{
    
    GameObject music = GameObject.FindGameObjectWithTag("MusicSource");
        if (music)
        {
            Destroy(music);

        }

}
    

}
