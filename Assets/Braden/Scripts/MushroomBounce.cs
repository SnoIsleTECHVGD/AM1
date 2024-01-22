using UnityEngine;

public class MushroomBounce : MonoBehaviour
{
    public float jumpPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Rigidbody2D playerBody = collision.gameObject.GetComponent<Rigidbody2D>();
        playerBody.AddForce(Vector2.up * (jumpPower * 250));
    }
}