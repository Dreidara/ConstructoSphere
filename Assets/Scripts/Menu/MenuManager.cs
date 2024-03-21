using ConstructoSphere.Gameplay;
using ConstructoSphere.Utilities;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public bool isUnlockAllLevels = false;

    public List<GameObject> panelsBG;


    private void Awake() => Instance = this;

    private void Start()
    {
        if (GameDataManager.Instance.isBackToHome)  
            ShowPanel(1);
        else 
            ShowPanel(0);
    }

    public void ShowPanel(int x)
    {
        foreach (GameObject item in panelsBG)
            item.SetActive(false);

        panelsBG[x].SetActive(true);
    }


    public void Quit()
	{
		if (Application.isEditor)
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }

		else
        {
			Application.Quit();
		}
	}



  

}
