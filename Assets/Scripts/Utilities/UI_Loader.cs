using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConstructoSphere.Main
{
    public class UI_Loader : MonoBehaviour
    {
        public static UI_Loader Instance { get; private set; }

        [SerializeField] GameObject _loadingCanvas;
        [SerializeField] TextMeshProUGUI _loaderText;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            _loadingCanvas.gameObject.SetActive(false);
        }

        public void Show()
        {
            _loaderText.text = "Please Wait";
            _loadingCanvas.gameObject.SetActive(true);
        }

        public void Show(string loadingText)
        {
            _loaderText.text = loadingText;
            _loadingCanvas.gameObject.SetActive(true);
        }

        public void Close()
        {
            _loadingCanvas.gameObject.SetActive(false);
        }

        public void LoadScene(Enum_SceneNames sceneName)
        {
            string actualSceneName = sceneName.ToString();
            StartCoroutine(LoadAsynchronously(actualSceneName, "Please Wait"));
        }

        public void LoadScene(Enum_SceneNames sceneName, string loadingText)
        {
            string actualSceneName = sceneName.ToString();
            StartCoroutine(LoadAsynchronously(actualSceneName, loadingText));
        }



        IEnumerator LoadAsynchronously(string sceneName, string loadingText)
        {
            Show(loadingText);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                yield return null;
            }
            Close();
        }
    }
}


