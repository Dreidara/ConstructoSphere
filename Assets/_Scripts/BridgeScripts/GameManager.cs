using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject completeLevelUI;
    public GameObject fallenUI;
    public GameObject tutorialUI;

    private bool isUIVisible = true;

    private void Start()
    {
        // Ensure the tutorial UI is initially visible
        tutorialUI.SetActive(true);
    }

    private void Update()
    {
        if (isUIVisible && (Input.anyKeyDown || MouseButtonDown()))
        {
            // Hide all UI elements when any key (excluding mouse clicks) is pressed
            completeLevelUI.SetActive(false);
            fallenUI.SetActive(false);
            tutorialUI.SetActive(false);
            isUIVisible = false;
        }
    }

    private bool MouseButtonDown()
    {
        // Check for mouse button clicks (left button or right button)
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        isUIVisible = true;
    }

    public void Fallen()
    {
        fallenUI.SetActive(true);
        isUIVisible = true;
    }
}
