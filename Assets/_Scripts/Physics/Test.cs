using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Rigidbody Rigidbody;

    // The object's velocity.
    private Vector3 velocity;

    private void Start()
    {
        // Set the Rigidbody's mass.
        Rigidbody.mass = 10.0f;

        // Set the object's initial velocity.
        velocity = Vector3.zero;
    }

    private void Update()
    {
        // Calculate the net force on the object.
        Vector3 netForce = Rigidbody.mass * Physics.gravity;

        // Apply the net force to the object.
        Rigidbody.AddForce(netForce);

        // Update the object's velocity.
        velocity += netForce * Time.deltaTime;

        // Update the object's position.
        Rigidbody.MovePosition(Rigidbody.transform.position + velocity * Time.deltaTime);
    }
}