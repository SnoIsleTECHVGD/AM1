using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float buildUp;
    public float maxSpeed;
    public float jumpspeed;

    private Rigidbody2D pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) == true)
        {
            pc.AddForce(Vector2.left * buildUp);
        }
        if (Input.GetKey(KeyCode.D) == true)
        {
            pc.AddForce(Vector2.right * buildUp);
        }
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            pc.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
        }
        pc.velocity = new Vector2(Mathf.Clamp(pc.velocity.x, -maxSpeed, maxSpeed), pc.velocity.y);
    }

}
