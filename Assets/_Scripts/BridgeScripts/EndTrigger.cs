using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource clappingSound; // Assign the Audio Source with the clapping sound

    private bool levelCompleted = false;

    void OnTriggerEnter()
    {
        if (!levelCompleted)
        {
            levelCompleted = true;
            PlayClappingSoundAndLoadScene();
        }
    }

    void PlayClappingSoundAndLoadScene()
    {
        gameManager.CompleteLevel(); // Trigger your completion logic

        if (clappingSound != null)
        {
            clappingSound.Play(); // Play the clapping sound
        }

        // Activate the cursor when the EndTrigger is triggered
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(DelayLoadScene(1, 3.0f)); // Load scene at index 1 after a 3-second delay
    }

    IEnumerator DelayLoadScene(int sceneIndex, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneIndex);
    }
}
