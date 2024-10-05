using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewAudioSettings", menuName = "Scriptable Objects/AudioSettings")]
public class AudioSettings : ScriptableObject
{	
    public AudioMixer mainMixer;
    
    [Tooltip("In Percentage"), Range(0.0001f, 1), SerializeField]
    float masterVolume = 0.5f, musicVolume = 1, sfxVolume = 1, uiVolume = 1, inGameEffectsVolume = 1;
    
    public float lowPassCutoffFrequency = 5000f;
    public float maxLowPassCutoffFrequency = 22000f;
    public float MasterVolume{  get { return masterVolume; }  set { masterVolume = value; SetMixerVolume("masterVolume",masterVolume); } }
    public float MusicVolume{  get { return musicVolume; }  set { musicVolume = value; SetMixerVolume("musicVolume",musicVolume); } }
    public float SFXVolume{  get { return sfxVolume; }  set { sfxVolume = value; SetMixerVolume("sfxVolume",sfxVolume); } }
    public float UIVolume{  get { return uiVolume; }  set { uiVolume = value; SetMixerVolume("uiVolume",uiVolume); } }
    public float InGameEffectsVolume{  get { return inGameEffectsVolume; }  set { inGameEffectsVolume = value; SetMixerVolume("inGameEffectsVolume",inGameEffectsVolume); } }
    
    
    private void Awake()
    {
        //These variables are found in the Audio mixer, then Exposed Parameters
        SetMixerVolume("masterVolume",masterVolume);
        SetMixerVolume("musicVolume",musicVolume);
        SetMixerVolume("sfxVolume",sfxVolume);
        SetMixerVolume("uiVolume",uiVolume);
        SetMixerVolume("inGameEffectsVolume",inGameEffectsVolume);
    }

    private void OnValidate()
    {
        SetMixerVolume("masterVolume",masterVolume);
        SetMixerVolume("musicVolume",musicVolume);
        SetMixerVolume("sfxVolume",sfxVolume);
        SetMixerVolume("uiVolume",uiVolume);
        SetMixerVolume("inGameEffectsVolume",inGameEffectsVolume);
    }
    
    /// <summary>
    /// This method converts the volume from a linear scale to a logarithmic scale, which is what the AudioMixer uses.
    /// </summary>
    /// <param name="floatName"></param>
    /// <param name="volume"></param>
    private void SetMixerVolume(string floatName, float volume)
    {
        mainMixer.SetFloat(floatName, Mathf.Log10(volume) * 20);
    }
    
    [Button]
    public void ActivateLowPassFilter()
    {
        mainMixer.SetFloat("masterCutoffFrequency", lowPassCutoffFrequency);
    }
    [Button]
    public void DeactivateLowPassFilter()
    {
        mainMixer.SetFloat("masterCutoffFrequency", maxLowPassCutoffFrequency);
    }
}
