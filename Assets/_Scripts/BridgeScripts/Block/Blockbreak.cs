using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blockbreak : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public float range;

    //breakBlock()
    public float block1, block2, block3;

    //BuildBlock()
    public GameObject placeBlock1, placeBlock2, placeBlock3;
    public bool select1, select2, select3;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            breakBlock();
        }

        //Key Inputs
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            select1 = true;
            select2 = false;
            select3 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            select1 = false;
            select2 = true;
            select3 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            select1 = false;
            select2 = false;
            select3 = true;
        }

        //Placement of Blocks
        if (Input.GetMouseButtonDown(1))
        {
            if (select1 && block1 > 0)
            {
                BuildBlock(placeBlock1);
                block1 -= 1;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (select2 && block2 > 0)
            {
                BuildBlock(placeBlock2);
                block2 -= 1;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (select3 && block3 > 0)
            {
                BuildBlock(placeBlock3);
                block3 -= 1;
            }
        }

    }

    private void breakBlock()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.name == "Block1" || hit.transform.name == "Block1 (Clone)")
            {
                Destroy(hit.transform.gameObject);
                block1 += 1;
            }
            if (hit.transform.name == "Block2" || hit.transform.name == "Block2 (Clone)")
            {
                Destroy(hit.transform.gameObject);
                block2 += 1;
            }
            if (hit.transform.name == "Block3" || hit.transform.name == "Block3 (Clone)")
            {
                Destroy(hit.transform.gameObject);
                block3 += 1;
            }
        }
    }

    private void BuildBlock(GameObject block)
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Block")
            {
                Vector3 spawnPose = new Vector3(Mathf.RoundToInt(hit.point.x + hit.normal.x / 2),
                    Mathf.RoundToInt(hit.point.y + hit.normal.y / 2),
                    Mathf.RoundToInt(hit.point.z + hit.normal.z / 2));
                Instantiate(block, spawnPose, Quaternion.identity);
            }
            else
            {
                Vector3 spawnPose = new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y), Mathf.RoundToInt(hit.point.z));
                Instantiate(block, spawnPose, Quaternion.identity);
            }
        }
    }
}
