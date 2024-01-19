using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerObjectPush : MonoBehaviour
{
    public KeyCode pushButton = KeyCode.E;

    public bool canPush = true;
    public bool isPushing = false;

    private GameObject activeObject;
    private Rigidbody2D activeBody;

    public GameObject player;

    private playerController controller;
    private Animator playerAnimator;
    private BoxCollider2D basePlayerCollider;
    private BoxCollider2D pushPlayerCollider;

    public Vector3 objectOffset;

    [SerializeField]
    private LayerMask groundMask;

    // Data
    private float throwDistance = 1.5f;
    private float idleThrowDistance = 0.5f;

    private float throwSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        controller = player.GetComponent<playerController>();
        playerAnimator = player.GetComponent<Animator>();
        basePlayerCollider = player.GetComponent<BoxCollider2D>();

        pushPlayerCollider = player.AddComponent<BoxCollider2D>();
        pushPlayerCollider.enabled = false;

        pushPlayerCollider.size = new Vector2(0.7492623f, 0.3579229f);
        pushPlayerCollider.offset = new Vector2(-0.04596132f, -0.2675037f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pushButton))
            if (isPushing == false)
                StartPushing();
            else
                StopPushing();

        // Set Box Position

        if (isPushing == true)
        {
            Vector3 newPosition = player.transform.position + objectOffset;
            activeBody.position = newPosition;
        }
    }

    // Coolissions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject") != true)
            return;

        activeObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == activeObject)
            activeObject = null;
    }

    // Main

    void StartPushing()
    {
        if (isPushing == true || canPush == false || !activeObject) // || !controller.CheckGrounding()
            return;

        isPushing = true;

        basePlayerCollider.enabled = false;
        pushPlayerCollider.enabled = true;

        activeBody = activeObject.GetComponent<Rigidbody2D>();
        activeBody.gravityScale = 0;
        //activeBody.rotation = 0;
        activeBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //print("Push " + activeObject.name + "!");
    }

    void StopPushing()
    {
        if (isPushing == true)
        {
            activeBody.constraints = RigidbodyConstraints2D.None;
            activeBody.gravityScale = 1;

            basePlayerCollider.enabled = true;
            pushPlayerCollider.enabled = false;

            // Throw

            bool isRight = playerAnimator.GetInteger("facedir") == 2;
            Vector2 throwVector;

            if (isRight == true)
                throwVector = Vector2.right;
            else
                throwVector = Vector2.left;

            if (playerAnimator.GetInteger("walkdir") == 0)
                activeBody.position += throwVector * idleThrowDistance;
            else
                activeBody.position += throwVector * throwDistance;

            activeBody.velocity = throwVector * throwSpeed;
            //print("STOP PUSH");
        }

        isPushing = false;
        activeObject = null;
        activeBody = null;
    }
}
