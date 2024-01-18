using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RedirectOnInput2 : MonoBehaviour
{
    public TMP_InputField answerInputField;

    // Add this method to the "On End Edit" event of your TMP_InputField in the Unity Editor.
    public void OnInputFieldEndEdit(string input)
    {
        Debug.Log("Input: " + input); // Add this line for debugging

        // Check if the answer is exactly "1300.7".
        if (input.Equals("1300.7"))
        {
            Debug.Log("Correct answer!"); // Add this line for debugging
            // Load the "City" scene.
            LoadCity2Scene();
        }
        else
        {
            Debug.Log("Incorrect answer: " + input); // Add this line for debugging
            // Optionally, you can handle incorrect input here (e.g., display an error message).
        }
    }

    // Call this method when you want to load the "City" scene.
    private void LoadCity2Scene()
    {
        SceneManager.LoadScene("City 2");
    }
}
