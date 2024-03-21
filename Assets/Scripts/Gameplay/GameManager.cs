using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace ConstructoSphere.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; set; }

        public Transform ObjectParent;


        public Transform UIObjectTransform;
        public GameObject UIObjectPrefab;

    
        public List<string> DragObjectsList = new List<string>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            int structureKey = GameDataManager.Instance.currentLevelId - 1;
            GameObject targetObjectTemp = Instantiate(GameDataManager.Instance.dataObjects[structureKey].TargetObjectPrefab, ObjectParent);
            GameObject dragObjectTemp = Instantiate(GameDataManager.Instance.dataObjects[structureKey].DragObjectPrefab, ObjectParent);

            for (int i = 0; i < targetObjectTemp.GetComponent<ListOfTargetObjects>().targetObjectsList.Count; i++)
            {
                DragObjectsList.Add(i.ToString());
            }

            for (int i = 0; i < dragObjectTemp.GetComponent<ListOfDragObjects>().dragObjects.Count; i++)
            {
                GameObject uiButton = Instantiate(UIObjectPrefab, UIObjectTransform);
                uiButton.GetComponent<SpawnDragObject>().DragObject = dragObjectTemp.GetComponent<ListOfDragObjects>().dragObjects[i];

                // Generate a random color with 150 alpha
                uiButton.GetComponent<Image>().color = new Color(Random.Range(0f, 1f),
                    Random.Range(0f, 1f), Random.Range(0f, 1f), 125 / 255f);

                DragObject dragObject = dragObjectTemp.GetComponent<ListOfDragObjects>().dragObjects[i].GetComponent<DragObject>();
                dragObject.key = i.ToString();
                dragObject.target = targetObjectTemp.GetComponent<ListOfTargetObjects>().targetObjectsList[i];
            }
        }


        public void RemoveDragObject(string keyToRemove)
        {
            DragObjectsList.RemoveAll(item => item == keyToRemove);


            if (DragObjectsList.Count == 0)
            {
                StartCoroutine(UIManager.Instance.StartViewMode());
            }
        }
     
    }
}
