using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private int previousSceneIndex; // Store the index of the previous scene

    private void Start()
    {
        // Initialize the previousSceneIndex to the current scene index when the script starts
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void toMenu()
    {
        Invoke("LoadMenuScene", 0.3f); // Load the menu scene with a 0.3-second delay
    }

    public void toScene1()
    {
        Invoke("LoadScene1", 0.3f); // Load scene 1 with a 0.3-second delay
    }

    public void toScene2()
    {
        Invoke("LoadScene2", 0.3f); // Load scene 2 with a 0.3-second delay
    }

    public void toScene3()
    {
        Invoke("LoadScene3", 0.3f); // Load scene 3 with a 0.3-second delay
    }

    public void toLevels()
    {
        Invoke("LoadLevelsScene", 0.3f); // Load the levels scene with a 0.3-second delay
    }

    public void toTuts()
    {
        Invoke("LoadTutsScene", 0.3f); // Load the tutorials scene with a 0.3-second delay
    }

    public void ReturnToPreviousScene()
    {
        Invoke("LoadPreviousScene", 0.3f); // Return to the previous scene with a 0.3-second delay
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadScene1()
    {
        SceneManager.LoadScene(2);
    }

    private void LoadScene2()
    {
        SceneManager.LoadScene(3);
    }

    private void LoadScene3()
    {
        SceneManager.LoadScene(4);
    }

    private void LoadLevelsScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadTutsScene()
    {
        SceneManager.LoadScene(5);
    }

    private void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousSceneIndex);
    }
}
