using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnController : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            Debug.Log("oough ow auuh");
            //camera fade out and in

            //respawn player

            //respawn game objects
        }
    }
}
