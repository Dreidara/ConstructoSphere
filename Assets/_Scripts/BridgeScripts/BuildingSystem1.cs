using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem1 : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject blockObject;

    public Transform parent;

    public Color normalColor;
    public Color highlightedColor;

    GameObject lastHightlightedBlock;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuildBlock(blockObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
    }

    void BuildBlock(GameObject block)
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            Vector3 spawnPosition;

            if (hitInfo.transform.tag == "Block")
            {
                // Offset by half the block size in each direction
                spawnPosition = hitInfo.point + hitInfo.normal * .5f;
            }
            else
            {
                spawnPosition = hitInfo.point;
            }

            Instantiate(block, spawnPosition, Quaternion.identity, parent);
        }
    }

    void DestroyBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block")
            {
                Destroy(hitInfo.transform.gameObject);
            }
        }
    }

    void HighlightBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block")
            {
                if (lastHightlightedBlock == null)
                {
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                }
                else if (lastHightlightedBlock != hitInfo.transform.gameObject)
                {
                    lastHightlightedBlock.GetComponent<Renderer>().material.color = normalColor;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                    lastHightlightedBlock = hitInfo.transform.gameObject;
                }
            }
        }
    }
}