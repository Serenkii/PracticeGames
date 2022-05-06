using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    

    public const float MAX_Y = 3.75f;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    KeyCode upKey;
    [SerializeField]
    KeyCode downKey;

    float yVelocity;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        yVelocity = 0;
        if (Input.GetKey(upKey) && Input.GetKey(downKey)) {
            return;
        }
        if (Input.GetKey(upKey))
        {
            yVelocity = 1;
            return;
        }
        if (Input.GetKey(downKey))
        {
            yVelocity = -1;
            return;
        }

    }

    private void FixedUpdate()
    {
        //rb.AddForce(new Vector2(0, speed * yVelocity), ForceMode2D.Impulse);
        rb.velocity = new Vector2(0, speed * yVelocity);
    }
}
