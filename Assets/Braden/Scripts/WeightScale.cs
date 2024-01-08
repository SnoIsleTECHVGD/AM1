using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightScale : MonoBehaviour
{
    [SerializeField]
    private GameObject weightBar;
    [SerializeField]
    private GameObject weightLeft;
    [SerializeField]
    private GameObject weightRight;

    private BoxCollider2D leftCollider;
    private BoxCollider2D rightCollider;

    private Vector3 baseLeftPos;
    private Vector3 baseRightPos;

    // Weight Data

    float maxDegrees = 35; // For any direction
    float transformYPerDegree = 0.0595f / 35;
    float minMultiplier = 1; // For the scale to tip in favor

    // Start is called before the first frame update
    void Start()
    {
        leftCollider = weightLeft.GetComponent<BoxCollider2D>();
        rightCollider = weightRight.GetComponent<BoxCollider2D>();

        baseLeftPos = weightLeft.transform.localPosition;
        baseRightPos = weightRight.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        SetScale();
    }

    // Main Functions

    float GetMass(BoxCollider2D collider)
    {
        RaycastHit2D[] cast = new RaycastHit2D[5];

        int results = collider.Cast(Vector2.up, cast, 1.5f, true);
        float mass = 1;

        if (results > 0)
        {
            for (int i = 0; i < results; i++)
            {
                RaycastHit2D hit = cast[i];

                if (hit.rigidbody.CompareTag("PushObject") || hit.rigidbody.CompareTag("Player"))
                {
                    mass += hit.rigidbody.mass;
                }
            }
        }

        return mass;
    }

    void SetScale()
    {
        float leftMass = GetMass(leftCollider);
        float rightMass = GetMass(rightCollider);

        // NOTE TO SELF: try constrainting the weightleft and weightright to the bar when its rotating, could work

        if (leftMass == rightMass)
        {
            weightBar.transform.rotation = new Quaternion(0, 0, 0, 0);
            //weightLeft.transform.localPosition = baseLeftPos;
           // weightRight.transform.localPosition = baseRightPos;

            return;
        }

        float ratio;
        int multiplier;

        if (leftMass > rightMass)
        {
            multiplier = 1;
            ratio = Mathf.Clamp((leftMass / rightMass) - minMultiplier, 0, 1);
        } else
        {
            multiplier = -1;
            ratio = Mathf.Clamp((rightMass / leftMass) - minMultiplier, 0, 1);
        }

        float weightBarRotate = (maxDegrees * ratio) * multiplier;
        float yDifference = weightBarRotate * transformYPerDegree;

        print(weightBarRotate);

        weightBar.transform.eulerAngles = new Vector3(0, 0, weightBarRotate);

        /*
        if (leftMass > rightMass)
        {
            weightLeft.transform.localPosition = new Vector3(baseLeftPos.x, baseLeftPos.y + yDifference, baseLeftPos.z);
            weightRight.transform.localPosition = new Vector3(baseRightPos.x, baseRightPos.y - yDifference, baseRightPos.z);
        } else
        {
            weightRight.transform.localPosition = new Vector3(baseRightPos.x, baseRightPos.y + yDifference, baseRightPos.z);
            weightLeft.transform.localPosition = new Vector3(baseLeftPos.x, baseLeftPos.y - yDifference, baseLeftPos.z);
        }*/
    }
}
