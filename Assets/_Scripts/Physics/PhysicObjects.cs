using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Rigidbody Rigidbody;

    private void Start()
    {
        // Set the Rigidbody's mass.
        Rigidbody.mass = 10.0f;

        // Set the gravity of the object.
        Rigidbody.AddForce(Physics.gravity * Rigidbody.mass);
    }

    private void Update()
    {
        // Apply physics to the game object.
        Rigidbody.AddForce(Physics.gravity * Rigidbody.mass);
    }
}