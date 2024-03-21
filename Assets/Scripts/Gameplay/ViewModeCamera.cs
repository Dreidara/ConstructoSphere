using ConstructoSphere.Gameplay;
using ConstructoSphere.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewModeCamera : MonoBehaviour
{
    [SerializeField] Image _centerDotImage;
    [SerializeField] Sprite _normalSprite; 
    [SerializeField] Sprite _detectedSprite;

    [SerializeField] float _sensitivity;
    [SerializeField] float _slowSpeed;
    [SerializeField] float _normalSpeed;
    [SerializeField] float _sprintSpeed;
    float currentSpeed;

    private void Update()
    {
        if (UIManager.Instance.GameState == Enum_GameState.ViewMode)
        {
            if (Input.GetMouseButton(1)) // If we are holding right click
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Movement();
                Rotation();

                // Raycast from the center dot
                if (_centerDotImage != null)
                {
                    Vector3 mousePosition = Input.mousePosition;
                    Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Handle raycast hit (e.g., interact with objects)
                        Debug.Log("Hit object: " + hit.collider.gameObject.name);
                        _centerDotImage.sprite = _detectedSprite;
                        _centerDotImage.transform.localScale = new Vector3(2f, 2f, 2f);
                    }

                    else
                    {
                        _centerDotImage.sprite = _normalSprite;
                        _centerDotImage.transform.localScale = Vector3.one;
                    }
                }
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _centerDotImage.sprite = _normalSprite;
                _centerDotImage.transform.localScale = Vector3.one;
            }
        }
    }

    private void Rotation()
    {
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * _sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    private void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = _sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = _slowSpeed;
        }
        else
        {
            currentSpeed = _normalSpeed;
        }

        transform.Translate(input * currentSpeed * Time.deltaTime);
    }
}
