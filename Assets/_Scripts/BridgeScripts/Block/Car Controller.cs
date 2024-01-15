using UnityEngine;

public class CarController : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    private Rigidbody rb;
    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            isMoving = true;
        }

        if (Input.GetKeyUp(KeyCode.KeypadPlus))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            // Always apply a forward force to the car based on its own direction.
            Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}