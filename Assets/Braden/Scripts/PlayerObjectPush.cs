using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerObjectPush : MonoBehaviour
{
    public KeyCode pushButton = KeyCode.E;

    public bool canPush = true;
    public bool isPushing = false;

    private Collider2D activeCollider;
    private GameObject activeObject;
    private Rigidbody2D activeBody;

    public GameObject player;
    private playerController controller;

    private float maxOffset = 1.2f;

    [SerializeField]
    private LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
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

        // Set Box Position

        if (isPushing == true)
        {
            Vector3 newPosition = player.transform.position + GetConstraintOffset(maxOffset);

            /*
            float rayMagnitude = 0.5f;

            RaycastHit2D hit = CastBoxRay(newPosition, rayMagnitude);

            if (hit.collider != null && hit.collider != activeCollider)
            {
                /*
                if (isRight == true)
                    rayMagnitude *= -1;

                newPosition = new Vector3(0, hit.point.y + rayMagnitude, 0);// new Vector3(hit.point.x + rayMagnitude, hit.point.y, 0);
            }*/

            activeBody.position = player.transform.position + GetConstraintOffset(maxOffset);
        }
    }

    // Coolissions

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject") != true)
            return;

        activeCollider = collision;
        activeObject = collision.gameObject;
    }

    // Helpers

    RaycastHit2D CastBoxRay(Vector3 origin, float magnitude)
    {
        Vector2 direction = Vector2.up;

        /*
        if (isRight == true)
            direction = Vector2.right;
        else
            direction = Vector2.left;*/

        return Physics2D.Raycast(origin, direction, magnitude, groundMask);
    }

    Vector3 GetConstraintOffset(float offsetNum)
    {
        Vector3 offset = new Vector3(0, offsetNum, 0); // new Vector3(offsetNum, -0.16f, 0);

        //if (isRight == false)
            //offset.x *= -1;

        return offset;
    }

    // Main

    void StartPushing()
    {
        if (isPushing == true || canPush == false || !activeObject || !controller.CheckGrounding())
            return;

        isPushing = true;

        //activeObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        activeBody = activeObject.GetComponent<Rigidbody2D>();
        activeBody.gravityScale = 0;
        activeBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Vector3 difference = activeObject.transform.position - player.transform.position;
        //isRight = difference.x >= 0;

        print("Push " + activeObject.name + "!");
    }

    void StopPushing()
    {
        if (isPushing == true)
        {
            //activeObject.layer = LayerMask.NameToLayer("Ground");
            activeBody.constraints = RigidbodyConstraints2D.None;
            activeBody.gravityScale = 1;
            print("STOP PUSH");
        }

        isPushing = false;
        activeCollider = null;
        activeObject = null;
        activeBody = null;
    }
}
