using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float buildUp;
    public float maxSpeed;
    public float jumpspeed;
    private Rigidbody2D pc;
    private Animator anim;
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
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float buildUpDelta = (buildUp * 1000) * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            pc.AddForce(Vector2.left * buildUpDelta);
            anim.SetInteger("walkdir", 1);
            
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pc.AddForce(Vector2.right * buildUpDelta);
            anim.SetInteger("walkdir", 2);
        }
        else
        {
            anim.SetInteger("walkdir", 0);
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
        hit = Physics2D.Raycast(foot.position, Vector2.down, 0.1f, groundMask);

        return hit;
    }

}
