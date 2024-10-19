using UnityEngine;
using UnityEngine.Audio;

namespace Gamersgrotto.Audio_system.Plugins {
    [CreateAssetMenu(fileName = "NewAudioSettings", menuName = "GamersGrotto/Audio System/AudioSettings")]
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
        public void Initialize() {
            LoadVolumes();
            SetMixerVolumes();
        }
        /// <summary>
        /// This method converts the volume from a linear scale to a logarithmic scale, which is what the AudioMixer uses.
        /// </summary>
        /// <param name="floatName"></param>
        /// <param name="volume"></param>
        private void SetMixerVolume(string floatName, float volume)
        {
            mainMixer.SetFloat(floatName, Mathf.Log10(volume) * 20);
            SaveVolume(floatName, volume);
        }
        void SetMixerVolumes() {
            //These variables are found in the Audio mixer, then Exposed Parameters
            SetMixerVolume("masterVolume",masterVolume);
            SetMixerVolume("musicVolume",musicVolume);
            SetMixerVolume("sfxVolume",sfxVolume);
            SetMixerVolume("uiVolume",uiVolume);
            SetMixerVolume("inGameEffectsVolume",inGameEffectsVolume);
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
    
    
        private void SaveVolume(string key, float volume)
        {
            PlayerPrefs.SetFloat(key, volume);
            PlayerPrefs.Save();
        
        }
    
        private void LoadVolumes()
        {
            masterVolume = PlayerPrefs.GetFloat("masterVolume", masterVolume);
            musicVolume = PlayerPrefs.GetFloat("musicVolume", musicVolume);
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume", sfxVolume);
            uiVolume = PlayerPrefs.GetFloat("uiVolume", uiVolume);
            inGameEffectsVolume = PlayerPrefs.GetFloat("inGameEffectsVolume", inGameEffectsVolume);
        }
    }
}
