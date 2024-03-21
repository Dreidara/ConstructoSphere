using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ConstructoSphere.Gameplay
{
    public class SpawnDragObject : MonoBehaviour
    {
        public GameObject DragObject;

        private void Start()
        {
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(SpawnObject);
        }

        private void SpawnObject()
        {
            DragObject.SetActive(true); 
            DragObject.transform.position = UIManager.Instance.SpawnPosition.position;
            DragObject.transform.rotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

    }

}

