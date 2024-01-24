using UnityEngine;

public class BridgeBuilder : MonoBehaviour
{
    public GameObject cubePrefab;
    public float gapBetweenCubes = 0.1f; // Adjust the gap between cubes as needed.

    private GameObject previousCube;

    void Start()
    {
        BuildBridge();
    }

    void BuildBridge()
    {
        Vector3 spawnPosition = Vector3.zero; // Initial position for the first cube.

        for (int i = 0; i < 10; i++) // Change 10 to the number of cubes you want to create.
        {
            GameObject newCube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
            newCube.transform.parent = transform; // Set the bridge as the parent.

            if (previousCube != null)
            {
                ConfigurableJoint joint = newCube.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousCube.GetComponent<Rigidbody>();

                // Adjust joint settings as needed.
                joint.xMotion = ConfigurableJointMotion.Limited;
                joint.yMotion = ConfigurableJointMotion.Limited;
                joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = ConfigurableJointMotion.Limited;
                joint.angularYMotion = ConfigurableJointMotion.Limited;
                joint.angularZMotion = ConfigurableJointMotion.Limited;

                SoftJointLimit linearLimit = new SoftJointLimit();
                linearLimit.limit = 0.1f; // Adjust these values as needed.
                joint.linearLimit = linearLimit;

                SoftJointLimitSpring linearLimitSpring = new SoftJointLimitSpring();
                linearLimitSpring.spring = 1000f; // Adjust these values as needed.
                linearLimitSpring.damper = 10f;
                joint.linearLimitSpring = linearLimitSpring;

                SoftJointLimit angularXLimit = new SoftJointLimit();
                angularXLimit.limit = 10f; // Adjust these values as needed.
                joint.lowAngularXLimit = angularXLimit;
                joint.highAngularXLimit = angularXLimit;

                SoftJointLimitSpring angularXLimitSpring = new SoftJointLimitSpring();
                angularXLimitSpring.spring = 1000f; // Adjust these values as needed.
                angularXLimitSpring.damper = 10f;
                joint.angularXLimitSpring = angularXLimitSpring;
            }

            previousCube = newCube;
            spawnPosition.x += newCube.transform.localScale.x + gapBetweenCubes; // Adjust for cube size and gap.
        }
    }
}
