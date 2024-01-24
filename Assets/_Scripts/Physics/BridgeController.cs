using UnityEngine;

public class BridgeController : MonoBehaviour
{
    public float massThreshold = 100.0f; // Adjust this to your desired mass threshold

    private FixedJoint[] joints;
    private bool isBroken = false;

    private void Start()
    {
        joints = GetComponentsInChildren<FixedJoint>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBroken)
            return; // Don't check further collisions if the bridge is already broken

        Rigidbody otherRigidbody = collision.rigidbody;

        if (otherRigidbody != null && otherRigidbody.mass >= massThreshold)
        {
            BreakBridge();
        }
    }

    private void BreakBridge()
    {
        foreach (FixedJoint joint in joints)
        {
            Destroy(joint); // Remove the Fixed Joint to break the bridge connection
        }

        isBroken = true;
    }

    // Add code to detect the player character and apply force to break the bridge
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isBroken)
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            if (playerRigidbody != null && playerRigidbody.mass >= massThreshold)
            {
                BreakBridge();
            }
        }
    }
}
