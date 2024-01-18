using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public float massThreshold = 5.0f; // Adjust this to your desired mass threshold
    public float forceThreshold = 10.0f; // Adjust this to your desired force threshold for breaking
    private bool simulationActive = false;
    private Rigidbody[] allRigidbodies;
    private FPSController playerController; // Reference to the FPSController script.
    private bool playerInBlockZone = false; // Added to track the player's presence in the trigger zone.

    private void Start()
    {
        allRigidbodies = GetComponentsInChildren<Rigidbody>();
        DeactivateSimulation();

        // Initialize the playerController reference.
        playerController = FindObjectOfType<FPSController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ToggleSimulationMode();
        }
    }

    private void ToggleSimulationMode()
    {
        simulationActive = !simulationActive;

        if (simulationActive)
        {
            ActivateSimulation();
        }
        else
        {
            DeactivateSimulation();
        }
    }

    private void ActivateSimulation()
    {
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    private void DeactivateSimulation()
    {
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (simulationActive)
        {
            Rigidbody otherRigidbody = collision.rigidbody;

            if (otherRigidbody != null && otherRigidbody.mass >= massThreshold && collision.relativeVelocity.magnitude >= forceThreshold)
            {
                // Call the ApplyForce function on the playerController.
                Vector3 forceToApply = new Vector3(0, 100.0f, 0); // Adjust the force as needed.
                playerController.ApplyForce(forceToApply);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && simulationActive)
        {
            // Do something when the player enters the trigger zone of the block.
            Debug.Log("Player entered the block!");
            playerInBlockZone = true; // Set playerInBlockZone to true when the player enters.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerInBlockZone) // Only log the exit if the player was previously inside.
            {
                Debug.Log("Player exited the block!");
            }
            playerInBlockZone = false;
        }
    }
}
