using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public bool setCameraOnRun = false;
    public float speed;
    public Vector3 offset;

    void Start()
    {
        if (setCameraOnRun == true)
        {
            transform.position = target.position + offset;
        }
    }

    // FixedUpdate runs more so its not laggy
    void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.fixedDeltaTime * speed) + offset;
        }
    }
}