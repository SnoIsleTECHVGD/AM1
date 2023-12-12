using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float buildUp;
    public float maxSpeed;
    public float jumpspeed;
    private Rigidbody2D pc;
    //Ground Detect
    [SerializeField]
    private Transform foot;
    [SerializeField]
    private LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        pc = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float buildUpDelta = (buildUp * 1000) * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            pc.AddForce(Vector2.left * buildUpDelta);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pc.AddForce(Vector2.right * buildUpDelta);
        }
        if (Input.GetKeyDown(KeyCode.Space) && CheckGrounding())
        {
            pc.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            gameObject.GetComponent<AudioSource>().Play();
        }

        pc.velocity = new Vector2(Mathf.Clamp(pc.velocity.x, -maxSpeed, maxSpeed), pc.velocity.y);

    }
    private bool CheckGrounding()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(foot.position, Vector2.down, 0.2f, groundMask);

        return hit;
    }

}
