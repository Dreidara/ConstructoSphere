using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed, sensitivity, maxForce;
    private Vector2 move, look;
    private float lookRotation;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //Find the target velocity
        Vector3 targetVelocity = new Vector3(move.x, 0, move.y);
        targetVelocity *= speed;

        //Align direction
        targetVelocity = transform.TransformDirection(targetVelocity);

        //Calculate Forces
        Vector3 velocityChange = (targetVelocity - rb.velocity);

        //Limit Force
        Vector3.ClampMagnitude(velocityChange, maxForce);

        // Apply the force to the rigidbody.
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        // Smooth out the velocity.
        rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity + velocityChange, Time.deltaTime * 5f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}