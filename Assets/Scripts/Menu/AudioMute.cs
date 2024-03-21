using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ConstructoSphere.Utilities;

public class AudioMute : MonoBehaviour
{
	public GameObject audioOnIcon;
	public GameObject audioOffIcon;
	public TextMeshProUGUI musicText;

	internal string audioMuteKey = DataManager.AudioMuted;

	private void OnEnable()
	{
		SetSoundState();
	}

	public void ToggleSound()
	{
		if (PlayerPrefs.GetInt(audioMuteKey, 0) == 0)
		{
			PlayerPrefs.SetInt(audioMuteKey, 1);
		}

		else
		{
			PlayerPrefs.SetInt(audioMuteKey, 0);
		}

		SetSoundState();
	}

	private void SetSoundState()
	{
		if (PlayerPrefs.GetInt(audioMuteKey, 0) == 0)
		{
			AudioListener.volume = 1;
			audioOnIcon.SetActive(true);
			audioOffIcon.SetActive(false);
			musicText.text = "AUDIO ON";
		}

		else
		{
			AudioListener.volume = 0;
			audioOnIcon.SetActive(false);
			audioOffIcon.SetActive(true);
			musicText.text = "AUDIO OFF";

		}
	}
}
