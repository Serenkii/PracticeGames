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
   
    private bool beginningMode;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        beginningMode = true;
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
        float randomY = Random.Range(0.1f, Mathf.Tan(maxBounceAngle));
        if (Random.value < 0.5f)
            randomY = -randomY;
        float x = 1;
        if (Random.value < 0.5f)
            x = -x;

        rb.velocity = new Vector2(x, randomY).normalized * initialSpeed;     
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

        if (beginningMode)
            return;

        if (! Mathf.Approximately(rb.velocity.sqrMagnitude, gameSpeed * gameSpeed))
        {

            rb.velocity = rb.velocity.normalized * gameSpeed;
        }
        
        if (Mathf.Abs(calculateVelocityAngle()) * Mathf.Rad2Deg > maxBounceAngle)
        {
            CorrectVelocity();
        }
        
    }

    //fix
    private void CorrectVelocity()
    {
        float y = Mathf.Tan(Mathf.Deg2Rad * maxBounceAngle);
        
        if (rb.velocity.y < 0)
        {
            y = -y;
        }
        rb.velocity = new Vector2(Mathf.Abs(rb.velocity.x) / rb.velocity.x, y).normalized * gameSpeed;
    }                              //either 1 or -1

    /// <summary>
    /// Calculates the angle of the velocity of the ball to either <code>Vector2.left</code> or <code>Vector2.right</code>
    /// 
    /// </summary>
    /// <returns>The signed angle. The angle is positive when the ball is travelling in positive x and positve y or in negative x and negative y direction. 
    /// Otherwise the angle is negative.</returns>
    private float calculateVelocityAngle()
    {
        return Mathf.Atan(rb.velocity.y / rb.velocity.x);
    }

    private bool OutOfBound()
    {
        return Mathf.Abs(this.transform.position.y) > 6 || Mathf.Abs(this.transform.position.x) > 12;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        //custom reflection only with the moving walls on the left and right
        if (!(collision.gameObject.CompareTag("MovingWall")))
            return;

        beginningMode = false;


        if (Mathf.Abs(collision.transform.position.x) + (Util.getMovingWallWidth() * 0.5)
            < Mathf.Abs(this.gameObject.transform.position.x))          //if ball is already behind front of moving wall
        {
            float temp = 1f;
            if (collision.gameObject.name == "WallLeft")
                temp = -1f;
            this.rb.velocity = new Vector2(temp * gameSpeed, 0f);       //Make the ball fly off the map in a straight line
            return;
        }


        float distanceToWallCenter = this.gameObject.transform.position.y - collision.transform.position.y;

        //calculate angle of return depending on distance to middle of wall
        float yValue = Mathf.Tan(Mathf.Deg2Rad * maxBounceAngle * 
            (distanceToWallCenter / ((Util.getMovingWallHeight() + this.gameObject.transform.localScale.y) * 0.5f)));
                                                                                //height of ball
        Vector2 velocityDirection = Vector2.left;       //if right wall was hit
        if (collision.gameObject.name == "WallLeft")    //if left wall was hit
            velocityDirection = Vector2.right;
        
        velocityDirection.y = yValue;

        this.rb.velocity = velocityDirection.normalized * gameSpeed;
    }
}
