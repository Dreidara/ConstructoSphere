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

    public void toScene4()
    {
        Invoke("LoadScene4", 0.3f); // Load scene 3 with a 0.3-second delay
    }

    public void toScene5()
    {
        Invoke("LoadScene5", 0.3f); // Load scene 3 with a 0.3-second delay
    }

    public void toScene6()
    {
        Invoke("LoadScene6", 0.3f); // Load scene 3 with a 0.3-second delay
    }

    public void toScene7()
    {
        Invoke("LoadScene7", 0.3f); // Load scene 3 with a 0.3-second delay
    }

    public void toScene8()
    {
        Invoke("LoadScene8", 0.3f); // Load scene 3 with a 0.3-second delay
    }
    public void toScene13()
    {
        Invoke("LoadScene13", 0.3f); // Load scene 3 with a 0.3-second delay
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
        SceneManager.LoadScene(11);
    }

    private void LoadScene2()
    {
        SceneManager.LoadScene(12);
    }

    private void LoadScene3()
    {
        SceneManager.LoadScene(2);
    }
    private void LoadScene4()
    {
        SceneManager.LoadScene(3);
    }
    private void LoadScene5()
    {
        SceneManager.LoadScene(4);
    }
    private void LoadScene6()
    {
        SceneManager.LoadScene(5);
    }
    private void LoadScene7()
    {
        SceneManager.LoadScene(8);
    }
    private void LoadScene8()
    {
        SceneManager.LoadScene(9);
    }
    private void LoadScene13()
    {
        SceneManager.LoadScene(13);
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
