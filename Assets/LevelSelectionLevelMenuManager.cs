using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionLevelMenuManager : MonoBehaviour
{
    public static int currLevel;
    public LevelObject[] levelObjects;
    public static int UnlockLevels;

    // Start is called before the first frame update
    void Start()
    {
        UnlockLevels = PlayerPrefs.GetInt("UnlockLevels", 1);

        // If UnlockLevels exceeds the total number of levels, reset to the maximum level
        if (UnlockLevels > levelObjects.Length)
        {
            UnlockLevels = levelObjects.Length;
            PlayerPrefs.SetInt("UnlockLevels", UnlockLevels);
        }

        currLevel = UnlockLevels; // Set currLevel to the unlocked levels
        UpdateLevelButtons();
    }

    // Update is called once per frame
    void Update()
    {
        // Your update logic here
    }

    public void UpdateLevelButtons()
    {
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if (i < currLevel)
            {
                levelObjects[i].levelButton.interactable = true;
            }
            else
            {
                levelObjects[i].levelButton.interactable = false;
            }
        }
    }
}
