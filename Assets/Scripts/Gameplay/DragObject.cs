using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructoSphere.Gameplay
{
    public class DragObject : MonoBehaviour
    {
        public string key;
        public GameObject target;
        public float snapDistance = 1.0f;
        private Vector3 mOffset;
        private float mZCoord;
        private bool isDragging = false;

        private void Start()
        {
            
        }

        private void OnMouseDown()
        {
            mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
            isDragging = true;
        }

        private void OnMouseDrag()
        {
            Vector3 newPosition = GetMouseAsWorldPoint() + mOffset;

            //newPosition.x = Mathf.Clamp(newPosition.x, -18f, 18f);

            //// Lock Y position to prevent going below ground level (0) or exceeding height limit (25)
            //newPosition.y = Mathf.Clamp(newPosition.y, 1f, 8f);

            transform.position = newPosition;
        }

        private Vector3 GetMouseAsWorldPoint()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = mZCoord;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }


        private void OnMouseUp()
        {
            isDragging = false;

            // Check the distance to the target
            if (Vector3.Distance(transform.position, target.transform.position) < snapDistance)
            {
                // Snap to the target
                transform.position = target.transform.position;
                target.gameObject.SetActive(false);
                GetComponent<BoxCollider>().enabled = false;
                GameManager.Instance.RemoveDragObject(key);
            }
        }
    }
}


