using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 5f;
    [SerializeField]
    private float maxSpeed = 10f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        float randomY = Random.Range(0.1f, 1f);
        if (Random.value < 0.5)
            randomY = -randomY;
        float randomX = Random.Range(0.1f, 2f);
        if (Random.value < 0.5)
            randomX = -randomX;

        rb.velocity = new Vector2(randomX, randomY);

        rb.velocity.Normalize();
        rb.velocity = rb.velocity * initialSpeed;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * Random.Range(rb.velocity.magnitude - 0.1f, rb.velocity.magnitude + 0.1f);
        
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;

        if (rb.velocity.magnitude < initialSpeed)
            rb.velocity = rb.velocity.normalized * initialSpeed;


    }
}
