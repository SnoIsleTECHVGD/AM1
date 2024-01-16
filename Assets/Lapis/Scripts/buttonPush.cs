using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPush : MonoBehaviour
{
    public GameObject pillar;

    public Vector3 changeVector;
    public float lerpSpeed;

    private Vector3 basePosition;
    private Vector3 goal = Vector3.zero;

    private float dt;

    private bool currentCollide = false;
    private bool isColliding = false;

    private void Start()
    {
        basePosition = pillar.transform.position;
    }

    private void Update()
    {
        if (goal == Vector3.zero)
            return;

        dt = Mathf.Clamp((Time.deltaTime / lerpSpeed) + dt, 0, 1);
        pillar.transform.position = Vector3.Lerp(basePosition, basePosition + goal, dt);

        if (dt == 1 && isColliding != currentCollide)
        {
            currentCollide = isColliding;
            basePosition = pillar.transform.position;

            goal = -goal;
            dt = 0;
        } 
    }

    // Events

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject"))
        {
            //dothething
            isColliding = true;

            if (goal == Vector3.zero)
            {
                currentCollide = true;
                goal = changeVector;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PushObject"))
        {
            //undothething
            isColliding = false;
        }
    }
}
