using UnityEngine;
using UnityEngine.Audio;
using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[Serializable]
public class Sound {
    [SerializeField] public string name;
    [SerializeField] public AudioMixerGroup audioMixerGroup;
    [SerializeField] public AudioClip clip;
    [Range(0f, 1f)] public float volume;
    [Range(.1f, 3f)] public float pitch = 1f;
    [SerializeField] public bool loop = false;
    [SerializeField] public bool playOnAwake = false;

    private AudioSource source;

    public void SetSource(AudioSource _source) {
        source = _source;
        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.playOnAwake = playOnAwake;
        source.loop = loop;
        source.outputAudioMixerGroup = audioMixerGroup;

    }

    public void Play() {
        source.Play();
    }

    public void Stop() {
        source.Stop();
    }

    public bool isPlaying() {
        if (source != null)
            return source.isPlaying;

        return false;
    }

}

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    public AudioMixer audioMixer;

    [Serializable]
    public class GameSounds {
        public string SoundName;
        [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "name" , NumberOfItemsPerPage = 50)] 
        public List<Sound> sounds = new List<Sound>();
    }

    [ListDrawerSettings(ShowIndexLabels = true, ListElementLabelName = "SoundName")] 
    public List<GameSounds> soundLists = new List<GameSounds>();

    private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

    private void Awake()
    {

        // Add all sounds to the dictionary
        foreach (GameSounds soundList in soundLists)
        {
            foreach (Sound sound in soundList.sounds)
                soundDictionary.Add(sound.name, sound);
        }



        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoadedHandler;
    }

    private void OnDestroy()
    {
        // Unregister the OnSceneLoadedHandler method from the sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoadedHandler;
    }

    private void OnSceneLoadedHandler(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {

        }

        else if (scene.buildIndex == 1)
        {

        }


    }

    private void Start() {

        //if (ES3.KeyExists(GameData.Instance.SetMusicVolumeUI))
        //{
        //    float musicValue = ES3.Load<float>(GameData.Instance.SetMusicVolumeUI);
        //    SetMusicVolume(musicValue);
        //}

        //if (ES3.KeyExists(GameData.Instance.SetSFXVolumeUI))
        //{
        //    float sfxValue = ES3.Load<float>(GameData.Instance.SetSFXVolumeUI);
        //    SetSoundEffectsVolume(sfxValue);
        //}


        // Create audio sources for each sound
        foreach (Sound sound in soundDictionary.Values) {
            GameObject go = new GameObject("Sound_" + sound.name);
            go.transform.SetParent(this.transform);
            sound.SetSource(go.AddComponent<AudioSource>());
        }

        PlaySound("AralApp_Theme");
    }

    public void SetMasterVolume(float masterLv) {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterLv) * 20);
    }

    public void SetMusicVolume(float vol) {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(vol) * 20);
        //ES3.Save(GameData.Instance.SetMusicVolumeUI, vol);
    }

    public void SetSoundEffectsVolume(float vol) {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(vol) * 20);
        //ES3.Save(GameData.Instance.SetSFXVolumeUI, vol);
    }

    public void PlaySound(string soundName) {
        if (soundDictionary.TryGetValue(soundName, out Sound sound)) {
            sound.Play();
        }
        else {
            Debug.LogWarning("AudioManager: Sound not found in list: " + soundName);
        }
    }

    public void StopSound(string soundName) {
        if (soundDictionary.TryGetValue(soundName, out Sound sound)) {
            sound.Stop();
        }
        else {
            Debug.LogWarning("AudioManager: Sound not found in list: " + soundName);
        }
    }

    // Getters for each sound list
    public List<Sound> GetBackgroundMusic() {
        foreach (GameSounds soundList in soundLists) {
            if (soundList.SoundName == "BackgroundMusic") {
                return soundList.sounds;
            }
        }

        return null;
    }

    public List<Sound> GetMusic() {
        foreach (GameSounds soundList in soundLists) {
            if (soundList.SoundName == "Music") {
                return soundList.sounds;
            }
        }

        return null;
    }

    public List<Sound> GetSFX() {
        foreach (GameSounds soundList in soundLists) {
            if (soundList.SoundName == "SFX") {
                return soundList.sounds;
            }
        }

        return null;
    }


    public List<Sound> GetSound(string soundName)
    {
        foreach (GameSounds soundList in soundLists)
        {
            if (soundList.SoundName == soundName)
            {
                return soundList.sounds;
            }
        }

        return null;
    }

    public void StopSoundsBySoundName(string soundName)
    {
        List<Sound> soundList = GetSound(soundName);

        foreach (Sound sound in soundList)
            sound.Stop();
    }

    public void StopAllGameUISounds() => StopSoundsBySoundName("Game UI");
    public void StopAllVoiceOverSounds() => StopSoundsBySoundName("Voicer Overs");

}
