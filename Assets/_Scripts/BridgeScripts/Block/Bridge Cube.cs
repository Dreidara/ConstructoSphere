using UnityEngine;

public class BridgeCube : MonoBehaviour
{
    private void Start()
    {
        // Ensure this script is attached to a cube with a Rigidbody component.
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("BridgeCube script requires a Rigidbody component on the GameObject.");
            return;
        }

        // Find the previous cube in the hierarchy to connect to.
        Transform previousCube = transform.parent?.GetChild(transform.GetSiblingIndex() - 1);

        if (previousCube != null)
        {
            // Create a FixedJoint component to connect to the previous cube's Rigidbody.
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = previousCube.GetComponent<Rigidbody>();
        }
    }
}
