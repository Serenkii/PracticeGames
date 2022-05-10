using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 200f;
    [SerializeField]
    private float gameSpeed = 400f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void Start()
    {
        setRandomStartingVelocity();
        setRandomStartingPosition();
    }
    
    void setRandomStartingVelocity()
    {
        float randomY = Random.Range(0.1f, 1f);
        if (Random.value < 0.5)
            randomY = -randomY;
        float randomX = Random.Range(0.1f, 2f);
        if (Random.value < 0.5)
            randomX = -randomX;

        rb.velocity = new Vector2(randomX, randomY);

        rb.velocity.Normalize();
        rb.velocity = rb.velocity * initialSpeed * Time.fixedDeltaTime;     //Time.fixedDeltaTime actually not needed here 
                                                                            //but I use it anyways to make value comparable to gameSpeed
    }

    void setRandomStartingPosition()
    {
        this.gameObject.transform.position = new Vector2(0, Random.Range(-3.5f, 3.5f));
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (OutOfBound())
        {
            setRandomStartingVelocity();
            setRandomStartingPosition();
            return;
        }


    }

    private bool OutOfBound()
    {
        return Mathf.Abs(this.transform.position.y) > 6 || Mathf.Abs(this.transform.position.x) > 12;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ball hit something");
    }
}
