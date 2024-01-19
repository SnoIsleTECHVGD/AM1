using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBrokenBot : MonoBehaviour
{
    private Animator myAnimator;
    private float yieldTime = 0;

    public float minTime = 3.5f;
    public float maxTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        yieldTime = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (yieldTime <= 0)
        {
            myAnimator.SetInteger("AnimState", Random.Range(1, 3));
            yieldTime = Random.Range(minTime, maxTime);
        } else
        {
            yieldTime -= Time.deltaTime;
        }
    }
}
