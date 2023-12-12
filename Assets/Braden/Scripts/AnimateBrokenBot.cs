using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBrokenBot : MonoBehaviour
{
    private Animator myAnimator;
    private float yieldTime = 0;

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
            yieldTime = Random.Range(3.5f, 5f);
        } else
        {
            yieldTime -= Time.deltaTime;
        }
    }
}
