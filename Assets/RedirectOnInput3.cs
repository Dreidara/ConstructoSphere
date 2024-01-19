using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RedirectOnInput3 : MonoBehaviour
{
    public TMP_InputField answerInputField;

    // Add this method to the "On End Edit" event of your TMP_InputField in the Unity Editor.
    public void OnInputFieldEndEdit(string input)
    {
        Debug.Log("Input: " + input); // Add this line for debugging

        // Check if the answer is exactly "1.73".
        if (input.Equals("1.73"))
        {
            Debug.Log("Correct answer!"); // Add this line for debugging
            // Load the "Nature" scene.
            LoadNatureScene();
        }
        else
        {
            Debug.Log("Incorrect answer: " + input); // Add this line for debugging
            // Optionally, you can handle incorrect input here (e.g., display an error message).
        }
    }

    // Call this method when you want to load the "City" scene.
    private void LoadNatureScene()
    {
        SceneManager.LoadScene("Nature");
    }
}
