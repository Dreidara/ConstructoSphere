using UnityEngine;

public class LevelSelectionLevelMenuManager : MonoBehaviour
{
    public static int currLevel;
    public LevelObject[] levelObjects;
    public static int UnlockLevels;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UnlockLevels = PlayerPrefs.GetInt("UnlockLevels", 0);
        for (int i = 0; i < levelObjects.Length; i++)
        {
            if(UnlockLevels >= i)
            {
                levelObjects[i].levelButton.interactable = true;
            }
        }
    }
}

