using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimSpeed : MonoBehaviour
{
    public float speed = 1;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.speed = speed;
    }

    void Update()
    {
        myAnimator.speed = speed;
    }
}
