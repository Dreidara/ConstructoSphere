using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    // Array to store all the Rigidbodies in the scene.
    private Rigidbody[] allRigidbodies;

    // Flag to indicate if simulation mode is active.
    private bool simulationActive = false;

    private void Start()
    {
        // Find all Rigidbodies in the scene.
        allRigidbodies = FindObjectsOfType<Rigidbody>();

        // Deactivate the Rigidbodies initially.
        DeactivateRigidbodies();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ToggleSimulationMode();
        }
    }

    // Function to toggle simulation mode.
    private void ToggleSimulationMode()
    {
        simulationActive = !simulationActive;

        if (simulationActive)
        {
            ActivateRigidbodies();
        }
        else
        {
            DeactivateRigidbodies();
        }
    }

    // Function to activate all Rigidbodies.
    private void ActivateRigidbodies()
    {
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    // Function to deactivate all Rigidbodies.
    private void DeactivateRigidbodies()
    {
        foreach (Rigidbody rb in allRigidbodies)
        {
            rb.isKinematic = true;
        }
    }
}