using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructoSphere.Gameplay
{
    public class ListOfDragObjects : MonoBehaviour
    {
        public List<GameObject> dragObjects;

        private void Start()
        {
            for (int i = 0; i < dragObjects.Count; i++)
            {
                dragObjects[i].SetActive(false);
            }
        }
    }

}

