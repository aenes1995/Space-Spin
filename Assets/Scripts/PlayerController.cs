using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Joystick joystick;
    public float speedLimit = 20f;

    public float thrust = 5f;

    [Range(0, 1)]
    public float sensivityRateOverSpeed = 0.2f;

    //this is the factor that increases sensitivity by speed.
    //it activates when you exceed the speed limit.


    Rigidbody2D rb;
    [HideInInspector]
    public float currentTopSpeed = 0f;
    [HideInInspector]
    public float appliedVelocitiy = 0f;    //joystick ile uyguladigimiz toplam hiz

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentTopSpeed = speedLimit;

    }

    void FixedUpdate()
    {

        if (rb.velocity.magnitude > speedLimit)
        //if player slow down by himself
        {
            currentTopSpeed = rb.velocity.magnitude;
        }

        else if (rb.velocity.magnitude < speedLimit)
        //slow down
        {
            currentTopSpeed = speedLimit;

        }

        if (Input.touchCount == 1)//touch screen
        {
            float horizontonal = joystick.Horizontal;
            float vertical = joystick.Vertical;


            Vector2 vec = new Vector2(horizontonal, vertical).normalized;
            if (rb.velocity.magnitude > speedLimit) vec = vec * thrust * Time.fixedDeltaTime * (Mathf.Abs(rb.velocity.magnitude) * sensivityRateOverSpeed);
            else vec = vec * thrust * Time.fixedDeltaTime;

            float v1 = rb.velocity.magnitude;
            rb.AddForce(vec, ForceMode2D.Impulse);
            float v2 = rb.velocity.magnitude;
            float acceleration = v2 - v1;
            appliedVelocitiy += acceleration;


            //we keep the applied velocity at "appliedVelocity"


            if (rb.velocity.magnitude > speedLimit && appliedVelocitiy > speedLimit)
            //if the player try to accelerating by himself and broke the speed limit
            {
                rb.velocity = new Vector2(rb.velocity.normalized.x, rb.velocity.normalized.y) * currentTopSpeed;
            }

        }

        else if (Input.touchCount > 1 && rb.velocity.magnitude > 0)
        {
            Vector2 tmp_vec = rb.velocity.normalized;
            rb.AddForce(new Vector2(-tmp_vec.x, -tmp_vec.y), ForceMode2D.Force);

            if (rb.velocity.magnitude < speedLimit)
            {
                currentTopSpeed = speedLimit;
            }
        }

    }
}
