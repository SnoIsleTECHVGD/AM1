using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectPush : MonoBehaviour
{
    public KeyCode pushButton = KeyCode.E;
    public bool isPushing = false;

    private Collider2D activeCollider;
    private GameObject activeObject;

    private BoxCollider2D myCollider;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pushButton))
            StartPushing();
    }

    // Coolissions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject") != true)
            return;

        activeCollider = collision;
        activeObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == activeCollider && isPushing == false)
            StopPushing();
    }

    // Main

    void StartPushing()
    {
        if (isPushing == true || !activeObject)
            return;

        print("Push " + activeObject.name);
    }

    void StopPushing()
    {
        if (isPushing == true)
        {
            // do stuff
        }

        isPushing = false;
        activeCollider = null;
        activeObject = null;
    }
}
