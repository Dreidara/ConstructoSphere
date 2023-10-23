using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move the sphere forward.
        rigidbody.AddForce(transform.forward * speed);
    }
}
