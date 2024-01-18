using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeight : MonoBehaviour
{
    public float force = 100f;
    public float offset;
    public float radius;

    public LayerMask LM;

    public List<GameObject> kinematicObjects;

    private void FixedUpdate()
    {
        var cols = Physics.OverlapSphere(transform.position - new Vector3(0, offset, 0), radius, LM);

        if (cols != null)
        {
            foreach (var col in cols)
            {
                Rigidbody rigiForced;

                // If the collider is dynamic, then apply the force to the rigidbody.
                if (col.TryGetComponent<Rigidbody>(out rigiForced) && !rigiForced.isKinematic)
                {
                    rigiForced.velocity = -transform.up * force;
                }
                // If the collider is kinematic, then apply the force to the kinematic object.
                else
                {
                    GameObject kinematicObject = kinematicObjects.Find(x => x == col.gameObject);
                    if (kinematicObject != null)
                    {
                        // Calculate the force to be applied.
                        Vector3 forceApplied = -transform.up * force * Time.deltaTime;

                        // Apply the force to the kinematic object.
                        kinematicObject.transform.position += forceApplied;
                    }
                }
            }
        }
    }
}