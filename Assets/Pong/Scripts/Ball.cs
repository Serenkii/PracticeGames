using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 15f;
    [SerializeField]
    private float gameSpeed = 25f;

    [Space]

    [SerializeField]
    private float maxBounceAngle = 45f;
    [SerializeField]
    private float movingWallHeight = 2.5f;              //(collision.gameObject.transform.localScale.y* 0.5f)

    private bool beginningMode = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        
    }

    void Start()
    {
        newRandomAttributes();
    }

    public void newRandomAttributes()
    {
        setRandomStartingVelocity();
        setRandomStartingPosition();
        beginningMode = true;
    }


    void setRandomStartingVelocity()
    {
        float randomY = Random.Range(0.1f, 1f);
        if (Random.value < 0.5f)
            randomY = -randomY;
        float randomX = Random.Range(0.1f, 2f);
        if (Random.value < 0.5f)
            randomX = -randomX;
        
        rb.velocity = new Vector2(randomX, randomY).normalized * initialSpeed;     
    }

    void setRandomStartingPosition()
    {
        this.gameObject.transform.position = new Vector2(0, Random.Range(-3.5f, 3.5f));
    }

    private void FixedUpdate()
    {
        if (OutOfBound())
        {
            newRandomAttributes();
            return;
        }


        if ((rb.velocity.sqrMagnitude < gameSpeed * gameSpeed - 0.01f || rb.velocity.sqrMagnitude > gameSpeed * gameSpeed + 0.01f) && !beginningMode)
        {
            rb.velocity = rb.velocity.normalized * gameSpeed;
            return;
        }
    }

    private bool OutOfBound()
    {
        return Mathf.Abs(this.transform.position.y) > 6 || Mathf.Abs(this.transform.position.x) > 12;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(collision.gameObject.CompareTag("MovingWall")))
            return;

        beginningMode = false;

        if (Mathf.Abs(collision.gameObject.transform.position.x) + (collision.gameObject.transform.localScale.x * 0.5) + 0.05
            < Mathf.Abs(this.gameObject.transform.position.x))          //if ball is already behind front of moving wall
        {
            float temp = 1f;
            if (collision.gameObject.name == "WallLeft")
                temp = -1f;
            this.rb.velocity = new Vector2(temp * gameSpeed, 0f);
            return;
        }


        float distanceToWallCenter = this.gameObject.transform.position.y - collision.transform.position.y;

        float yValue = Mathf.Tan(Mathf.Deg2Rad * maxBounceAngle * 
            (distanceToWallCenter / ((movingWallHeight + this.gameObject.transform.localScale.y) * 0.5f)));

        Vector2 velocityDirection = Vector2.left;
        if (collision.gameObject.name == "WallLeft")
            velocityDirection = Vector2.right;
        
        velocityDirection.y = yValue;

        this.rb.velocity = velocityDirection.normalized * gameSpeed;
    }
}
