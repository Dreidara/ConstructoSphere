using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonClickSound : MonoBehaviour, IPointerClickHandler
{
    private AudioSource buttonClickSound; // Reference to the AudioSource component on "ButtonSounds"

    private void Start()
    {
        // Find the AudioSource component on the "ButtonSounds" GameObject
        buttonClickSound = GameObject.Find("ButtonSounds").GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play the button click sound when the button is clicked
        if (buttonClickSound != null)
        {
            buttonClickSound.Play();
        }
        // Add any other button-click related functionality here
    }
}
