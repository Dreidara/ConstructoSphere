using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public void OnLevelComplete()
    {
        if (LevelSelectionLevelMenuManager.currLevel == LevelSelectionLevelMenuManager.UnlockLevels)
        {
            LevelSelectionLevelMenuManager.UnlockLevels++;
            PlayerPrefs.SetInt("UnlockLevels", LevelSelectionLevelMenuManager.UnlockLevels);
        }
        SceneManager.LoadScene("LevelMainMenu");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
