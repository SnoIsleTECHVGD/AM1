using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerObjectPush : MonoBehaviour
{
    public KeyCode pushButton = KeyCode.E;
    public bool isPushing = false;

    private Collider2D activeCollider;
    private GameObject activeObject;

    private BoxCollider2D myCollider;
    private ParentConstraint constraint;

    public GameObject player;
    private playerController controller;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
        controller = player.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pushButton))
            if (isPushing == false)
                StartPushing();
            else
                StopPushing();
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
        if (isPushing == true || !activeObject || !controller.CheckGrounding())
            return;

        isPushing = true;

        ConstraintSource source = new ConstraintSource();
        source.sourceTransform = player.transform;
        source.weight = 1;

        constraint = activeObject.AddComponent<ParentConstraint>();
        constraint.constraintActive = true;
        constraint.SetTranslationOffset(constraint.AddSource(source), activeObject.transform.position - player.transform.position);

        print("Push " + activeObject.name + "!");
    }

    void StopPushing()
    {
        if (isPushing == true)
        {
            Destroy(constraint);
        }

        isPushing = false;
        activeCollider = null;
        activeObject = null;
    }
}
