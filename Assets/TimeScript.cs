using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class TimeScript : MonoBehaviour
{
    public float timeRemaining = 1200; // 20 minutes in seconds
    public bool timeIsRunning = true;
    public TMP_Text timeText;
    public GameObject timeUpPanel; // Reference to the UI panel

    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
        timeUpPanel.SetActive(false); // Make sure the panel is initially hidden
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0;
            timeIsRunning = false;
            DisplayTime(0); // Ensure the UI displays 00:00 when time runs out
            ShowTimeUpPanel(); // Show the "Time's Up" panel
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay <= 0)
        {
            timeToDisplay = 0;
            timeIsRunning = false;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void ShowTimeUpPanel()
    {
        timeUpPanel.SetActive(true); // Show the "Time's Up" panel
    }
}
