using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConstructoSphere.Gameplay
{
    public class GameDataManager : MonoBehaviour
    {
        [System.Serializable]
        public class StructuresDataObjects
        {
            public string name;
            public GameObject TargetObjectPrefab;
            public GameObject DragObjectPrefab;
        }

        public static GameDataManager Instance { get; set; }

        public bool isBackToHome = false;
        public int currentLevelId;
        public List<StructuresDataObjects> dataObjects;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }

}

