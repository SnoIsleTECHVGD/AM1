using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorAni : MonoBehaviour
{
    private Animator anim;
    private bool doorUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            //do the thign
            anim.SetBool("KEy", true);
            doorUnlocked = true;
        }
        else if (collision.CompareTag("Player"))
        {
            anim.SetBool("doorOpen", true);
        }
    }
}
