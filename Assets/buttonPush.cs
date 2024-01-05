using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPush : MonoBehaviour
{
    public GameObject pillar;
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
        if (collision.gameObject.CompareTag("PushObject"))
        {
            Debug.Log("pushed");
            //dothething
            pillar.transform.Translate(Vector3.up);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject"))
        {
            Debug.Log("unpushed");
            //undothething
            pillar.transform.Translate(Vector3.down);
        }
    }
}
