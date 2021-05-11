using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuckPhysics : MonoBehaviour
{
    public Rigidbody2D rb;
    public float topSpeed = 20f;
    private bool wallhit = false;
    private bool playerhit = false;

    private Vector2 wallNormal;

    private float goalBounce = 1.5f;
    private float playerBounce = 1.25f;
    private float wallDamp = 0.9f;
    private float friction = 0.999f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //TODO: possible spricting friction
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity.magnitude);
        //Top speed on puck
        rb.velocity = rb.velocity * friction;
        CheckTopSpeed();

    }

    private void CheckTopSpeed()
    {
        if (rb.velocity.magnitude > topSpeed)
        {
            rb.velocity = rb.velocity.normalized * topSpeed;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "border")
        {
            rb.velocity = rb.velocity * wallDamp;
            wallhit = true;
            wallNormal = collision.GetContact(0).normal;
        }

        if (collision.collider.tag == "goal")
        {
            rb.velocity = rb.velocity * goalBounce;
            //TODO: put a slightly random angle in
        }

        if(collision.collider.tag == "Player")
        {
            rb.velocity = rb.velocity * playerBounce;
            playerhit = true;
        }

        //cheeck topspeed
        CheckTopSpeed();

    }
}
