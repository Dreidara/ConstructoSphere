using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConstructoSphere.Gameplay
{
    public class MovementCameraManager : MonoBehaviour
    {
       [SerializeField] Transform _targetCamera;
       [SerializeField] float _rotationSpeed = 30.0f; 
       [SerializeField] float _maxRotation = 360.0f;  
       [SerializeField] float _minRotation = 0.0f;

       [SerializeField] float _zoomSpeed = 5f;
       [SerializeField] float _maxZoom = 360.0f;
       [SerializeField] float _minZoom = 0.0f;

       [SerializeField] KeyCode _rotateClockwiseButton = KeyCode.A;          
       [SerializeField] KeyCode _rotateCounterClockwiseButton = KeyCode.D;    
       [SerializeField] KeyCode _zoomCounterIncrease = KeyCode.W;    
       [SerializeField] KeyCode _zoomCounterDecrease = KeyCode.S;    

        private bool _isRotatingClockwise = false;
        private bool _isRotatingCounterClockwise = false;
        private bool _isZoomIncrease = false;
        private bool _isZoomDecrease = false;


        private void Update()
        {
            // Check if the clockwise rotation button is pressed
            if (Input.GetKey(_rotateClockwiseButton) || _isRotatingClockwise)
            {
                RotateCamera(1);
            }
            // Check if the counterclockwise rotation button is pressed
            else if (Input.GetKey(_rotateCounterClockwiseButton) || _isRotatingCounterClockwise)
            {
                RotateCamera(-1);
            }

            // Check if the zoom increase button is pressed
            if (Input.GetKey(_zoomCounterIncrease) || _isZoomIncrease)
            {
                ZoomCamera(-1);
            }
            // Check if the zoom decrease button is pressed
            else if (Input.GetKey(_zoomCounterDecrease) || _isZoomDecrease)
            {
                ZoomCamera(1);

            }
        }


        private void RotateCamera(int direction)
        {
            float newRotation = _targetCamera.rotation.eulerAngles.y + direction * _rotationSpeed * Time.deltaTime;
            newRotation = newRotation % 360;
            _targetCamera.rotation = Quaternion.Euler(_targetCamera.rotation.eulerAngles.x, newRotation, _targetCamera.rotation.eulerAngles.z);
        }



        private void ZoomCamera(float value)
        {
            Camera cameraComponent = _targetCamera.transform.GetChild(0).GetComponent<Camera>();
            float adjustedSpeed = _zoomSpeed * value;
            cameraComponent.fieldOfView += adjustedSpeed;
            cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, _minZoom,_maxZoom);
        }

    }

}

