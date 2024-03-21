using ConstructoSphere.Utilities;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ConstructoSphere.LevelSelect
{
    public class LevelSelectManager : MonoBehaviour
    {
        public static LevelSelectManager Instance { get;  set; }

        public Transform LevelTransformParent;
        public GameObject LevelPrefab;

        [Space, Title("Stars Colors")]
        [SerializeField] Color defaultStarColor;
        [SerializeField] Color starColor;

        private void Awake() => Instance = this;

        public IEnumerator Start()
        {
            string keyName = DataManager.LevelUnlock;
            int levelModeUnlock = PlayerPrefs.GetInt(keyName, 1);

            for (int i = 0; i < 10; i++)
            {
                GameObject levelButton = Instantiate(LevelPrefab, LevelTransformParent);
                levelButton.GetComponent<LevelSelectButton>().SetLevelID(i + 1);

                if (i + 1 > levelModeUnlock)
                {
                    if (MenuManager.Instance.isUnlockAllLevels == false)
                    {
                        levelButton.GetComponent<Button>().interactable = false;
                        //Instantiate(lockGameObject, levelButtons[i].transform);
                    }
                }
            }

            yield return new WaitForEndOfFrame();
        }

        public void SetStars(GameObject[] starsObjects, int stars)
        {
            for (int i = 0; i < starsObjects.Length; i++)
            {
                if (i < stars)
                {
                    starsObjects[i].GetComponent<Image>().color = starColor;

                }
                else
                {
                    starsObjects[i].GetComponent<Image>().color = defaultStarColor;
                }
            }
        }
    }

}

