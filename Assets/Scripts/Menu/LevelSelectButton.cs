using ConstructoSphere.Gameplay;
using ConstructoSphere.Main;
using ConstructoSphere.Utilities;
using EasyUI.Helpers;
using EasyUI.Popup;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ConstructoSphere.LevelSelect
{
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] private int _levelId;
        [SerializeField] private GameObject[] _starsObj;
        [SerializeField] private TextMeshProUGUI levelText;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (GameDataManager.Instance.dataObjects.Count < _levelId)
                {
                    Popup.Show(Enum_EasyUIKeys.Key_1);
                    return;
                }

                GameDataManager.Instance.isBackToHome = true;
                GameDataManager.Instance.currentLevelId = _levelId;
                UI_Loader.Instance.LoadScene(Enum_SceneNames._Game);
            });

            SetStars();
        }


        public void SetLevelID(int id)
        {
            _levelId = id;
            levelText.text = _levelId.ToString();
        }

        private void SetStars()
        {
            string _keyname = DataManager.LevelStars;
            int _starsCount = PlayerPrefs.GetInt($"{_keyname}{_levelId}");
            LevelSelectManager.Instance.SetStars(_starsObj, _starsCount);
        }
    }

}
