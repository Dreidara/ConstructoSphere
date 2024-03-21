using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Loading : MonoBehaviour {

	[SerializeField] private Image loadingBar;
	[SerializeField] private TextMeshProUGUI loadingText;
	[SerializeField] private float currentAmount;
	[SerializeField] private float speed;

	AsyncOperation async = null;
	bool loadOnce = false;


	private void Update()
	{
		if (async != null)
			return;

		if (currentAmount < 100)
		{
			currentAmount += speed * Time.deltaTime;
			loadingText.text = "Loading ..... " + ((int)currentAmount).ToString() + "%";
		}

		else
		{
			if (!loadOnce)
			{
				loadOnce = true;
				StartCoroutine(LoadTheGame());
			}

		}

		loadingBar.fillAmount = currentAmount / 100;

	}

	IEnumerator LoadTheGame()
	{
		loadOnce = true;

		loadingText.text = "Checking .....";
		yield return new WaitForSeconds(0.5f);
		loadingText.text = "Processing .....";
		yield return new WaitForSeconds(0.5f);
		loadingText.text = "Starting .....";
		yield return new WaitForSeconds(0.5f);
		loadingText.text = "Iniatilizing ARCHIFACT .....";
		yield return new WaitForSeconds(1f);
		async = SceneManager.LoadSceneAsync(ConstructoSphere.Main.Enum_SceneNames._MainMenu.ToString());
		yield return async;

	}
}
