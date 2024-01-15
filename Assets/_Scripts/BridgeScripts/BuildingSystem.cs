using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildSystem : MonoBehaviour
{
    public Block[] availableBuildingBlocks;
    int currentBlockIndex = 0;

    Block currentBlock;
    [SerializeField] private TMP_Text blockNameText; // Use [SerializeField] to ensure proper serialization

    public Transform shootingPoint;
    GameObject blockObject;

    public Transform parent;

    public Color normalColor;
    public Color highlightedColor;

    GameObject lastHighlightedBlock;

    private bool isCursorLocked = true; // Initial state: locked

    private void Start()
    {
        SetText();
        ToggleCursorState(isCursorLocked);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BuildBlock(currentBlock.BlockObject);
        }
        if (Input.GetMouseButtonDown(1))
        {
            DestroyBlock();
        }
        HighlightBlock();
        ChangeCurrentBlock();

        if (Input.GetKeyDown(KeyCode.F1)) // Toggle cursor state when the F1 key is pressed
        {
            ToggleCursorState(!isCursorLocked);
        }
    }

    void ChangeCurrentBlock()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0)
        {
            currentBlockIndex++;
            if (currentBlockIndex > availableBuildingBlocks.Length - 1)
            {
                currentBlockIndex = 0;
            }
        }
        else if (scroll < 0)
        {
            currentBlockIndex--;
            if (currentBlockIndex < 0)
            {
                currentBlockIndex = availableBuildingBlocks.Length - 1;
            }
        }
        currentBlock = availableBuildingBlocks[currentBlockIndex];
        SetText();
    }

    void SetText()
    {
        blockNameText.text = currentBlock.BlockName + "\n" + currentBlock.AmmountOfItemNeeded + " x " + currentBlock.ItemsNeededForBuildingBlock;
    }

    void BuildBlock(GameObject block)
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block")
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x + hitInfo.normal.x / 2), Mathf.RoundToInt(hitInfo.point.y + hitInfo.normal.y / 2), Mathf.RoundToInt(hitInfo.point.z + hitInfo.normal.z / 2));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);

                // Play the audio clip
                GetComponent<AudioSource>().Play(); // Assuming the Audio Source is attached to the same GameObject
            }
            else
            {
                Vector3 spawnPosition = new Vector3(Mathf.RoundToInt(hitInfo.point.x), Mathf.RoundToInt(hitInfo.point.y), Mathf.RoundToInt(hitInfo.point.z));
                Instantiate(block, spawnPosition, Quaternion.identity, parent);

                // Play the audio clip
                GetComponent<AudioSource>().Play(); // Assuming the Audio Source is attached to the same GameObject
            }
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

    void ToggleCursorState(bool lockCursor)
    {
        isCursorLocked = lockCursor;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HighlightBlock()
    {
        if (Physics.Raycast(shootingPoint.position, shootingPoint.forward, out RaycastHit hitInfo))
        {
            if (hitInfo.transform.tag == "Block")
            {
                if (lastHighlightedBlock == null)
                {
                    lastHighlightedBlock = hitInfo.transform.gameObject;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                }
                else if (lastHighlightedBlock != hitInfo.transform.gameObject)
                {
                    lastHighlightedBlock.GetComponent<Renderer>().material.color = normalColor;
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.color = highlightedColor;
                    lastHighlightedBlock = hitInfo.transform.gameObject;
                }
            }
            else if (lastHighlightedBlock != null)
            {
                lastHighlightedBlock.GetComponent<Renderer>().material.color = normalColor;
                lastHighlightedBlock = null;
            }
        }
    }
}
