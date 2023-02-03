using System;
using UnityEngine;

using Ensign.Unity;
public class SoundManager : Singleton<SoundManager>
{
    [HideInInspector] public static float volume;
    [SerializeField] private Sound[] _sounds;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        LoadSounds();
    }
    public void Start() { 
        LoadVolume(); 
    }

    /// <summary>
    /// Play sound with EActionSound
    /// </summary>
    public void PlaySound(EActionSound name)
    {
        ChangeVolume(); 
        Sound sound = Array.Find(_sounds, Sound => Sound.Name == name);
        if(sound == null)
        {
            Debug.LogWarning($"Sound: {name} not found!");
            return;
        }
        sound.Source.PlayOneShot(sound.Clip);
    }

    private void LoadSounds()
    {
        foreach (Sound s in _sounds)
        {
            if(s.Source == null){
                s.Source = gameObject.AddComponent<AudioSource>();
            }
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    /// <summary>
    /// Play sound with name
    ///</summary>
    public void PlayThisSound(string clipName)
    {
        AudioSource audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot((AudioClip)Resources.Load("Sounds/" + clipName, typeof(AudioClip)));
    }
        
    private void ChangeVolume() { AudioListener.volume = volume; }
        
    private void LoadVolume() { volume = PlayerPrefs.GetFloat("musicVolume"); }
        
    public void SaveVolume() { PlayerPrefs.SetFloat("musicVolume", volume); }
}

[System.Serializable]
public class Sound
{
    public EActionSound Name;
    public AudioClip Clip;
    public AudioSource Source;
    [Range(0f, 1f)]
    public float Volume;
    [Range(0f, 1f)]
    public float Pitch;
    public bool Loop;
}

public enum EActionSound
{
    PlayerMove,
    PlayerDie,
    GameOver,
    WinGame,
    Button,
    SwitchUI,
    CarBackground,
    CarBeep01,
    CarBeep02,
    CarBeep03
}