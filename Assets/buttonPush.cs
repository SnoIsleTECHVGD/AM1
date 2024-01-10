using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPush : MonoBehaviour
{
    public GameObject pillar;
    public Vector3 changeVector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject"))
        {
            Debug.Log("pushed");
            //dothething
            pillar.transform.Translate(changeVector);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject"))
        {
            Debug.Log("unpushed");
            //undothething
            pillar.transform.Translate(-changeVector);
        }
    }
}
