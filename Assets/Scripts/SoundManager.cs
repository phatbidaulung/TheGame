using UnityEngine;
using Ensign;
using Ensign.Unity;
public class SoundManager : Singleton<SoundManager>
{
    public static float volume;
    public void Start() { LoadVolume(); }
        
    private void Update() { ChangeVolume(); }

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