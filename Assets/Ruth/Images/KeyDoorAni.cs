using UnityEngine;

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
            //do the thign
            anim.SetBool("KEy", true);
            doorUnlocked = true;

            keySound.Play();
            Destroy(GameObject.Find("Key"));
           
            print("working");
        }
        else if (collision.gameObject.CompareTag("Player") && doorUnlocked)
        {
            anim.SetBool("doorOpen", true);
            doorOpenSound.Play();

            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody2D>().simulated = false;

            print("working2");
        }
    }
}
