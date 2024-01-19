using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteScript : MonoBehaviour
{
    public void OnLevelComplete()
    {
        // Increment currLevel before checking the condition
        LevelSelectionLevelMenuManager.currLevel++;

        Debug.Log("currLevel: " + LevelSelectionLevelMenuManager.currLevel);
        Debug.Log("UnlockLevels: " + LevelSelectionLevelMenuManager.UnlockLevels);

        // Check if the current level is level 8
        if (LevelSelectionLevelMenuManager.currLevel == 8)
        {
            // Reset the progression back to the initial values
            LevelSelectionLevelMenuManager.currLevel = 1;
            LevelSelectionLevelMenuManager.UnlockLevels = 1;
            PlayerPrefs.SetInt("UnlockLevels", LevelSelectionLevelMenuManager.UnlockLevels);
            PlayerPrefs.SetInt("currLevel", LevelSelectionLevelMenuManager.currLevel);
        }
        else if (LevelSelectionLevelMenuManager.currLevel > LevelSelectionLevelMenuManager.UnlockLevels)
        {
            // Unlock the next level when the current level is completed
            LevelSelectionLevelMenuManager.UnlockLevels = LevelSelectionLevelMenuManager.currLevel;
            PlayerPrefs.SetInt("UnlockLevels", LevelSelectionLevelMenuManager.UnlockLevels);
        }

        SceneManager.LoadScene("LevelMainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Your start logic here
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }
}
