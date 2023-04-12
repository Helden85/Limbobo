using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //[SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerhealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerhealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        // Check if check point available;
        if(currentCheckpoint == null)
        {
            // Show game over screen
            uiManager.GameOver();
            Physics2D.gravity = new Vector3(0, -9.8F, 0);

            return; // Don't execute the rest of this function
        }

        transform.position = currentCheckpoint.position; // Move player to checkpoint position
        playerhealth.Respawn(); // Restore player health and reset animation

        // Move camera to checkpoint room (** for this to work the checkpoint objects has to placed
        // as a child of the room object)
        //Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform; // Store the checkpoint that we activated as the current one
            collision.GetComponent<Collider2D>().enabled = false; // Deactivate checkpoint collider
        }
    }
}
