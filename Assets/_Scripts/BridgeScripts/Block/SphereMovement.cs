using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public float speed = 10f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Move the sphere forward.
        _rigidbody.AddForce(transform.forward * speed);
    }
}
