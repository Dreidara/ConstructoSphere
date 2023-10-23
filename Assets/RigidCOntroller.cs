using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidCOntroller : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;

    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayerCamera()
    {

    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;

        // Get the current position of the PlayerBody object.
        Vector3 currentPosition = PlayerBody.transform.position;

        // Calculate the new position of the PlayerBody object.
        Vector3 newPosition = currentPosition + MoveVector;

        // Move the PlayerBody object to the new position.
        PlayerBody.GetComponent<Rigidbody>().MovePosition(newPosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }
}
