using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSound : MonoBehaviour
{
    public AudioSource itemSound;
    public AudioClip itemSoundClip;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            itemSound.PlayOneShot(itemSoundClip);
            gameObject.SetActive(false);
        }
    }
}
