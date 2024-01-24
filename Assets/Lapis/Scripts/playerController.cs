using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerController : MonoBehaviour
{
    // Options
    public float buildUp;
    public float maxSpeed;
    public float pushMaxSpeed;

    public float jumpspeed;
    public float coyoteTime;

    // Private References
    [SerializeField]
    private PlayerObjectPush objectPush;

    public Rigidbody2D pc;
    private Animator anim;

    // Private Data

    private float timeSinceGrounded = 0;
    private bool isJumping = false;
    private float jumpTime = 0;

    // Ground Detect
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
        // Apply Force

        float buildUpDelta = (buildUp * 1000) * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            pc.AddForce(Vector2.left * buildUpDelta);
            anim.SetInteger("walkdir", 1);
            anim.SetInteger("facedir", 1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            pc.AddForce(Vector2.right * buildUpDelta);
            anim.SetInteger("walkdir", 2);
            anim.SetInteger("facedir", 2);
        }
        else
        {
            anim.SetInteger("walkdir", 0);
        }

        anim.SetBool("isPushing", objectPush.isPushing);
        CheckGrounding();

        if (isJumping == true)
        {
            jumpTime += Time.deltaTime;

            if (jumpTime >= 0.25f && timeSinceGrounded == 0)
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false && timeSinceGrounded <= coyoteTime) // && objectPush.isPushing == false
        {
            isJumping = true;
            jumpTime = 0;

            pc.AddForce(Vector2.up * jumpspeed, ForceMode2D.Impulse);
            gameObject.GetComponent<AudioSource>().Play();
        }

        float currentMaxSpeed = maxSpeed;

        if (objectPush.isPushing == true)
        {
            currentMaxSpeed = pushMaxSpeed;
        }

        pc.velocity = new Vector2(Mathf.Clamp(pc.velocity.x, -currentMaxSpeed, currentMaxSpeed), pc.velocity.y);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Climbable"))
        {
            if (Input.GetKey(KeyCode.W))
            {
                float buildUpDelta = (buildUp * 600) * Time.deltaTime;
                pc.AddForce(Vector2.up * buildUpDelta);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            //if fall into hazard just reload scene lmao i definitely did NOT ctrl+a the entire script on accident that didnt happen :) -L
            StartCoroutine(hidePlayer());
        }
    }
    IEnumerator hidePlayer()
    {
        yield return new WaitForSeconds(0.15f);
        GetComponent<SpriteRenderer>().enabled = false;
        pc.simulated = false;

        StartCoroutine(reloadLevel());
    }

    IEnumerator reloadLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1");
    }

    public void CheckGrounding()
    {
        RaycastHit2D hit = Physics2D.Raycast(foot.position, Vector2.down, 0.1f, groundMask);

        if (hit)
        {
            timeSinceGrounded = 0;
        } else
        {
            timeSinceGrounded += Time.deltaTime;
        }
    }
}
