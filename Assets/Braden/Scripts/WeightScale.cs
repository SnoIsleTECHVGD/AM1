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

    private Transform leftTrans;
    private Transform rightTrans;
    private Transform barTrans;

    private Vector3 basePos;

    // Weight Data

    public float maxDegrees = 25; // For any direction
    public float minMultiplier = 4; // For the scale to tip in favor

    float transformYPerDegree = 0.07f / 35;
    float transformXPerDegree = 0.025f / 35;

    // Start is called before the first frame update
    void Start()
    {
        leftCollider = weightLeft.GetComponent<BoxCollider2D>();
        rightCollider = weightRight.GetComponent<BoxCollider2D>();

        leftTrans = weightLeft.transform;
        rightTrans = weightRight.transform;
        barTrans = weightBar.transform;

        basePos = rightTrans.localPosition;
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

        int results = collider.Cast(Vector2.up, cast, 0.4f, true);
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

        float dt = Time.deltaTime * 3;

        if (leftMass == rightMass)
        {
            barTrans.rotation = Quaternion.Lerp(barTrans.rotation, Quaternion.Euler(0, 0, 0), dt);
            leftTrans.localPosition = Vector2.Lerp(leftTrans.localPosition, new Vector2(-basePos.x, basePos.y), dt);
            rightTrans.localPosition = Vector2.Lerp(rightTrans.localPosition, new Vector2(basePos.x, basePos.x), dt);

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

        float xDiff = -(transformXPerDegree * weightBarRotate);
        float yDiff = weightBarRotate * transformYPerDegree;

        barTrans.rotation = Quaternion.Lerp(barTrans.rotation, Quaternion.Euler(0, 0, weightBarRotate), dt);

        if (leftMass > rightMass)
        {
            leftTrans.localPosition = Vector2.Lerp(leftTrans.localPosition, new Vector2(-basePos.x, basePos.y - yDiff), dt);
            rightTrans.localPosition = Vector2.Lerp(rightTrans.localPosition, new Vector2(xDiff + basePos.x, basePos.y + yDiff), dt);
        } else
        {
            leftTrans.localPosition = Vector2.Lerp(leftTrans.localPosition, new Vector2(xDiff - basePos.x, basePos.y - (yDiff - 0.007f)), dt);
            rightTrans.localPosition = Vector2.Lerp(rightTrans.localPosition, new Vector2(basePos.x, basePos.y + yDiff), dt);
        }
    }
}
