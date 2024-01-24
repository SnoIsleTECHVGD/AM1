using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyDoorAni : MonoBehaviour
{
    private Animator anim;
    private bool doorUnlocked = false;

    [SerializeField]
    private AudioSource keySound;
    [SerializeField]
    private AudioSource doorOpenSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key") && collision.gameObject.GetComponentInParent<ObjectData>().isBeingHeld == false)
        {
            // put key in door
            anim.SetBool("KEy", true);
            doorUnlocked = true;

            keySound.Play();
            Destroy(GameObject.Find("Key"));
        }
        else if (collision.gameObject.CompareTag("Player") && doorUnlocked)
        {
            // open door
            anim.SetBool("doorOpen", true);
            doorOpenSound.Play();

            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;

            StartCoroutine(winScene());
        }
    }

    IEnumerator winScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("WinScene");
    }
}
