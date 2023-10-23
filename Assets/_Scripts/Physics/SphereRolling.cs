using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRolling : MonoBehaviour
{
    public float speed = 5.0f;

    private void Update()
    {
        Rigidbody rb = GetComponent < Rigidbody>();
        Vector3 movement = new Vector3(0, 0, -1); // Reverse the direction by using -1 along the Z-axis.

        // Apply force in the reverse direction along the Z-axis to make the sphere roll backward.
        rb.AddForce(movement * speed * rb.mass);
    }
}