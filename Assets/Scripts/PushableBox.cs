using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBox : MonoBehaviour
{
    [SerializeField]
    private float pushPower = 0.5f;

    [SerializeField]
    private float dragWhenNotPushed = 5.0f;

    [SerializeField]
    private float increasedGravityScale = 4.5f;

    private Rigidbody2D rb;
    private bool isBeingPushed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = increasedGravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isBeingPushed = true;
        Vector2 pushDirection = new Vector2(collision.contacts[0].normal.y, -collision.contacts[0].normal.x) * pushPower;
        rb.velocity = pushDirection;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isBeingPushed = false;
    }

    private void Update()
    {
        if (!isBeingPushed)
        {
            rb.drag = dragWhenNotPushed;
        }
        else
        {
            rb.drag = 0;
        }
    }
}
