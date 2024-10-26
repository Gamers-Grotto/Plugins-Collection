using GamersGrotto.Core;
using GamersGrotto.Core.Extended_Attributes;
using GamersGrotto.Core.ScriptablePreferences.PreferenceTypes;
using UnityEngine;
using UnityEngine.Audio;

namespace GamersGrotto.Audio_System 
{
    [CreateAssetMenu(fileName = "NewAudioSettings", menuName = Constants.AudioSystemPath + "AudioSettings")]
    public class AudioSettings : ScriptableObject
    {	
        public AudioMixer mainMixer;

        [SerializeField, ShowInInspector] private FloatScriptablePreference masterVolume;
        [SerializeField, ShowInInspector] private FloatScriptablePreference musicVolume;
        [SerializeField, ShowInInspector] private FloatScriptablePreference sfxVolume;
        [SerializeField, ShowInInspector] private FloatScriptablePreference uiVolume;
    
        public float lowPassCutoffFrequency = 5000f;
        public float maxLowPassCutoffFrequency = 22000f;
        
        public float MasterVolume
        {  
            get => masterVolume.value;
            set
            {
                masterVolume.value = value; 
                SetMixerVolume(masterVolume.Key, value);
            } 
        }
        
        public float MusicVolume
        {  
            get => musicVolume.value;
            set
            {
                musicVolume.value = value; 
                SetMixerVolume(musicVolume.Key, musicVolume.value);
            } 
        }
        
        public float SfxVolume
        {
            get => sfxVolume.value;
            set
            {
                sfxVolume.value = value; 
                SetMixerVolume("sfxVolume", sfxVolume.value);
            } 
        }

        public float UIVolume
        {
            get => uiVolume.value;
            set
            {
                uiVolume.value = value; 
                SetMixerVolume("uiVolume",uiVolume.value);
            }
        }
        
        public void Initialize() 
        {
            LoadVolumes();
            SetMixerVolumes();
        }
    
        private void LoadVolumes()
        {
            masterVolume.Load();
            musicVolume.Load();
            sfxVolume.Load();
            uiVolume.Load();
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

        private void SetMixerVolumes() 
        {
            //These variables are found in the Audio mixer, then Exposed Parameters
            SetMixerVolume(masterVolume.Key, masterVolume.value);
            SetMixerVolume(musicVolume.Key, musicVolume.value);
            SetMixerVolume(sfxVolume.Key, sfxVolume.value);
            SetMixerVolume(uiVolume.Key, uiVolume.value);
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
}
