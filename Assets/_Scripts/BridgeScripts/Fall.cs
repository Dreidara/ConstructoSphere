using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fall : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource fallenSound; // Assign the Audio Source with the fallen sound

    private bool levelCompleted = false;

    void OnTriggerEnter()
    {
        if (!levelCompleted)
        {
            levelCompleted = true;
            PlayFallenSoundAndRestartScene();
        }
    }

    void PlayFallenSoundAndRestartScene()
    {
        gameManager.Fallen(); // Trigger your completion logic

        if (fallenSound != null)
        {
            fallenSound.Play(); // Play the fallen sound
        }

        // Activate the cursor when the Fall trigger is triggered
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        string currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(DelayLoadScene(currentSceneName, 2.0f)); // Restart the current scene after a 3-second delay
    }

    IEnumerator DelayLoadScene(string sceneName, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName);
    }
}
