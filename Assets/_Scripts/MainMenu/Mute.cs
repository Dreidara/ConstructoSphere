using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource[] audioSources; // Add your audio sources to this array in the Inspector.
    public Text muteButtonText; // Add a reference to the button's text component in the Inspector.

    private bool isMuted = false;

    private void Start()
    {
        // Initialize the mute button text.
        UpdateMuteButtonText();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;

        // Toggle the mute state of all audio sources.
        foreach (var audioSource in audioSources)
        {
            audioSource.mute = isMuted;
        }

        // Update the mute button text.
        UpdateMuteButtonText();
    }

    private void UpdateMuteButtonText()
    {
        // Update the mute button text based on the mute state.
        if (isMuted)
        {
            muteButtonText.text = "Unmute";
        }
        else
        {
            muteButtonText.text = "Mute";
        }
    }
}
