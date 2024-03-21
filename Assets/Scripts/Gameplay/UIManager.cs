using ConstructoSphere.Main;
using com.spacepuppy.Collections;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ConstructoSphere.Utilities;

namespace ConstructoSphere.Gameplay
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; set; }

        public Enum_GameState GameState;

        public Transform SpawnPosition;

        [Title("CANVAS")]
        [System.Serializable] public class StringGameObjectsDict : SerializableDictionaryBase<string, GameObject> { }
        public StringGameObjectsDict gameObjectsCanvas;

        [Title("GAME UTILITIES")]
        public GameObject targetCamera;
        public GameObject viewModeCamera;
        public TextMeshProUGUI currentModelText;

        [Title("COUNTDOWN START")]
        [SerializeField] int _countdownStartTime = 5;
        [SerializeField] TextMeshProUGUI _countdownText;

        [Title("GAME TIMER")]
        [SerializeField] TextMeshProUGUI _timerText;
        [SerializeField] float _totalTime;
        float _currentTime;
        private float _maxTime = 180;
        private int _starRating;

        [Title("PAUSE UTILITIES")]
        [SerializeField] Button _resumeButton;
        [SerializeField] Button _restartButton;
        [SerializeField] Button _backToHomeButton;

        [Title("SUCCESS UTILITIES")]
        [SerializeField] Button _nextLevelButton;
        //[SerializeField] Button _viewModeButton;
        [SerializeField] Button _backToHomeButton1;

        [Title("FAILED UTILITIES")]
        [SerializeField] Button _restartButton1;
        [SerializeField] Button _backToHomeButton2;

        [Title("VIEW MODE UTILITIES")]
        [SerializeField] Button _nextLevelButton1;

        [Space, Title("Stars")]
        [SerializeField] GameObject[] starsObjects;
        [SerializeField] Color defaultStarColor;
        [SerializeField] Color starColor;


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _currentTime = _totalTime;

            StartCoroutine(StartCountDown());

            AssignListener(_resumeButton, Resume);
            AssignListener(_restartButton, Restart);
            AssignListener(_backToHomeButton, BackToHome);
            AssignListener(_nextLevelButton, NextLevel);
            //AssignListener(_viewModeButton, ViewMode);
            AssignListener(_backToHomeButton1, BackToHome);
            AssignListener(_restartButton1, Restart);
            AssignListener(_backToHomeButton2, BackToHome);
            // AssignListener(_nextLevelButton1, NextLevel);

            if (Application.isEditor)
                _countdownStartTime = 0;

            int structureKey = GameDataManager.Instance.currentLevelId - 1;
            currentModelText.text = GameDataManager.Instance.dataObjects[structureKey].name;
        }

        private void AssignListener(Button button, UnityAction action)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(action);
        }


        private void Update()
        {
            if (GameState == Enum_GameState.Playing)
            {
                if (_currentTime > 0)
                {
                    _currentTime -= Time.deltaTime;
                    UpdateTimerDisplay();
                }
                else
                {
                    Debug.Log("Time's up!");
                    GameState = Enum_GameState.Failed;
                    ShowCanvas("Failed");
                }
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameState == Enum_GameState.Playing)
                {
                    Pause();
                }
                else if (GameState == Enum_GameState.None)
                {
                    Resume();
                }
            }
        }

        private void UpdateTimerDisplay()
        {
            int minutes = Mathf.FloorToInt(_currentTime / 60);
            int seconds = Mathf.FloorToInt(_currentTime % 60);
            _timerText.text = "TIME: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        [ContextMenu("Win")]
        public void UpdateStarRating()
        {
            if (_currentTime > _maxTime)
            {
                _starRating = 3;
            }
            else if (_currentTime > _maxTime / 2)
            {
                _starRating = 2;

            }
            else
            {
                _nextLevelButton.gameObject.SetActive(false);
                _starRating = 1;
            }

            SetStars(_starRating);

            string _keyNameStars = DataManager.LevelStars;
            int currentLevelID = GameDataManager.Instance.currentLevelId;
            string key = $"{_keyNameStars}{currentLevelID}";

            // Get the current saved star rating
            int currentStarRating = PlayerPrefs.GetInt(key);

            // Only save the new star rating if it's greater than the current saved star rating
            if (_starRating > currentStarRating)
            {
                PlayerPrefs.SetInt(key, _starRating);
            }

            string keyNameLevelUnlock = DataManager.LevelUnlock;
            int nextLevelToUnlock = GameDataManager.Instance.currentLevelId + 1;
            PlayerPrefs.SetInt($"{keyNameLevelUnlock}" , nextLevelToUnlock);
        }

        private IEnumerator StartCountDown()
        {
            ShowCanvas("Countdown");
            GameState = Enum_GameState.Starting;

            int structureKey = GameDataManager.Instance.currentLevelId - 1;
            _countdownText.text = GameDataManager.Instance.dataObjects[structureKey].name;
            yield return new WaitForSeconds(1);

            _countdownText.text = "Game Starting in ....";
            yield return new WaitForSeconds(1);

            while (_countdownStartTime > 0)
            {
                _countdownText.text = _countdownStartTime.ToString();
                yield return new WaitForSeconds(1);
                _countdownStartTime--;
            }

            _countdownText.text = "Start Build!";
            yield return new WaitForSeconds(1);
            _countdownText.gameObject.SetActive(false);

            GameState = Enum_GameState.Playing;
            ShowCanvas("Gameplay");

        }

        public void ShowCanvas(string key)
        {
            foreach (KeyValuePair<string, GameObject> item in gameObjectsCanvas)
            {
                item.Value.SetActive(false);
            }

            gameObjectsCanvas[key].SetActive(true);
        }


        private void Pause()
        {
            GameState = Enum_GameState.None;
            ShowCanvas("Pause");
        }

        private void Resume()
        {
            GameState = Enum_GameState.Playing;
            ShowCanvas("Gameplay");
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void BackToHome()
        {
            UI_Loader.Instance.LoadScene(Enum_SceneNames._MainMenu);
        }


        public IEnumerator StartViewMode()
        {
            UI_Loader.Instance.Show("Starting View Mode . . . .");
            yield return new WaitForSeconds(1f);
            UI_Loader.Instance.Close();
            ViewMode();
        }


        private void ViewMode()
        {
            ShowCanvas("ViewMode");
            GameState = Enum_GameState.ViewMode;
            targetCamera.gameObject.SetActive(false);
            viewModeCamera.gameObject.SetActive(true);
        }

        private void NextLevel()
        {
            //if (GameDataManager.Instance.currentLevelId == 3)
            //    return;

            GameDataManager.Instance.currentLevelId += 1;
            UI_Loader.Instance.LoadScene(Enum_SceneNames._Game);
        }

      
        private void SetStars(int stars)
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


