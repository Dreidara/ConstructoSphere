using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float gravity = 10f;

    private Rigidbody rigidbody_;
    private bool isFlying = false;

    void Start()
    {
        rigidbody_ = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // If the player presses the spacebar twice, then start flying.
        if (Input.GetKeyDown(KeyCode.Space) && !isFlying)
        {
            isFlying = true;
        }

        // If the player is flying, then apply a force upwards.
        if (isFlying)
        {
            rigidbody_.AddForce(Vector3.up * speed);

            // Allow the player to fly faster by holding down the left shift key.
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed *= 2;
            }

            // Move the player forward manually.
            transform.position += transform.forward * speed;
        }

        // If the player is not flying, then apply gravity.
        else
        {
            rigidbody_.AddForce(Vector3.down * gravity);
        }
    }
}
