using UnityEngine;
using UnityEngine.SceneManagement;

public class GraphicsManager : MonoBehaviour
{
    public void low()
    {
        QualitySettings.SetQualityLevel(0);
        UpdateGraphicsUI(); // Update UI to reflect changes
    }

    public void mid()
    {
        QualitySettings.SetQualityLevel(1);
        UpdateGraphicsUI(); // Update UI to reflect changes
    }

    public void high()
    {
        QualitySettings.SetQualityLevel(2);
        UpdateGraphicsUI(); // Update UI to reflect changes
    }

    private void UpdateGraphicsUI()
    {
        // Add code here to update your UI elements to reflect the current graphics settings
    }

    private void Awake()
    {
        GraphicsManager[] managers = FindObjectsOfType<GraphicsManager>();

        if (managers.Length > 1 && managers[0] != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
