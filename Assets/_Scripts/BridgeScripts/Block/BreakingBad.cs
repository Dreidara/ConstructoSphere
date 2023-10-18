using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BreakingBad : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    public float range;

    //breakBlock()
    public float block1, block2, block3;

    //BuildBlock()
    public GameObject placeBlock1, placeBlock2, placeBlock3;
    public bool select1, select2, select3;

    //Pang UI
    public Image imageToChange;
    public TextMeshProUGUI textToChange;
    public Sprite SandSprite, ConcreteSprite, WoodSprite, DefaultImageSprite;

    private bool isCursorVisible = false; // Start with the cursor hidden

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            breakBlock();
        }

        //Key Inputs
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            select1 = !select1;
            select2 = false;
            select3 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            select1 = false;
            select2 = !select2;
            select3 = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            select1 = false;
            select2 = false;
            select3 = !select3;
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

        //F1 Keycode
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (isCursorVisible)
            {
                HideCursor(); // Hide the cursor when F1 is pressed
            }
            else
            {
                ShowCursor(); // Show the cursor when F1 is pressed again
            }
        }

        //Key for deletion
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            breakBlock();
        }

        //Change UI Elements 
        ChangeUIElements();
    }
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
        isCursorVisible = false;
    }

    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None; // Set the cursor to be movable
        Cursor.visible = true; // Make the cursor visible
        isCursorVisible = true;
    }

    void ChangeUIElements()
    {
        if (select1)
        {
            imageToChange.sprite = SandSprite;
            textToChange.text = $"Sand {block1}x"; // Display block type and count
        }
        else if (select2)
        {
            imageToChange.sprite = ConcreteSprite;
            textToChange.text = $"Concrete {block2}x"; // Display block type and count
        }
        else if (select3)
        {
            imageToChange.sprite = WoodSprite;
            textToChange.text = $"Wood {block3}x"; // Display block type and count
        }
        else
        {
            // Set default image and text when no block is selected
            imageToChange.sprite = DefaultImageSprite;
            textToChange.text = "Select a block";
        }
    }

    private void breakBlock()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            if (hit.transform.name == "Block1" || hit.transform.name == "Block1(Clone)")
            {
                Destroy(hit.transform.gameObject);
                block1 += 1;
            }
            if (hit.transform.name == "Block2" || hit.transform.name == "Block2(Clone)")
            {
                Destroy(hit.transform.gameObject);
                block2 += 1;
            }
            if (hit.transform.name == "Block3" || hit.transform.name == "Block3(Clone)")
            {
                Destroy(hit.transform.gameObject);
                block3 += 1;
            }
        }
        ChangeUIElements();
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
