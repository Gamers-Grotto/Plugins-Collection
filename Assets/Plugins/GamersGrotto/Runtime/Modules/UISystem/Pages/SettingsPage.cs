using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Pages
{
    public class SettingsPage : Page
    {
        [Header("Audio Settings")]
        public UnityEvent<float> masterVolumeChanged;
        public UnityEvent<float> musicVolumeChanged;
        public UnityEvent<float> effectsVolumeChanged;
        
        private float masterVolumePref;
        private float musicVolumePref;
        private float effectsVolumePref;
        
        private void Start()
        {
            masterVolumePref = PlayerPrefs.GetFloat(nameof(masterVolumePref), 1f);
            musicVolumePref = PlayerPrefs.GetFloat(nameof(musicVolumePref), 1f);
            effectsVolumePref = PlayerPrefs.GetFloat(nameof(effectsVolumePref), 1f);
        }

        protected override IEnumerator DrawUI(VisualElement root)
        {
            Debug.Log("Settings Page");

            var background = Create("full-screen");
            root.Add(background);
            
            var settingsMenuPanel = CreateAudioSettingsPanel();
            background.Add(settingsMenuPanel);
            
            #region Absolute
            var backButton = CreateButton("Back", () => UIManager.Instance.GoBack());
            backButton.AddToClassList("bottom-right-button");
            root.Add(backButton);
            #endregion
            
            yield break;
        }

        private Box CreateAudioSettingsPanel()
        {
            var settingsMenuTitle = CreateLabel("Settings", "menu-title");

            var masterSliderLabel = CreateLabel("Master Volume", "default-text");
            var masterSlider = CreateSlider(0f, 1f);
            masterSlider.SetValueWithoutNotify(masterVolumePref);
            masterSlider.RegisterValueChangedCallback(OnMasterVolumeChanged);
            var masterVolumeSliderContainer = Create("labeled-slider-container");
            masterVolumeSliderContainer.Add(masterSliderLabel);
            masterVolumeSliderContainer.Add(masterSlider);
            
            var musicVolumeSliderContainer = Create("labeled-slider-container");
            var musicLabel = CreateLabel("Music", "default-text");
            var musicSlider = CreateSlider(0f, 1f);
            musicSlider.SetValueWithoutNotify(musicVolumePref);
            musicSlider.RegisterValueChangedCallback(OnMusicVolumeChanged);
            musicVolumeSliderContainer.Add(musicLabel);
            musicVolumeSliderContainer.Add(musicSlider);
            
            var effectsVolumeSliderContainer = Create("labeled-slider-container");
            var effectsLabel = CreateLabel("Effects", "default-text");
            var effectsSlider = CreateSlider(0f, 1f);
            effectsSlider.SetValueWithoutNotify(effectsVolumePref);
            effectsSlider.RegisterValueChangedCallback(OnEffectsVolumeChanged);
            effectsVolumeSliderContainer.Add(effectsLabel);
            effectsVolumeSliderContainer.Add(effectsSlider);
            
            var settingsMenuPanel = CreateBox();
            settingsMenuPanel.Add(settingsMenuTitle);
            settingsMenuPanel.Add(masterVolumeSliderContainer);
            settingsMenuPanel.Add(musicVolumeSliderContainer);
            settingsMenuPanel.Add(effectsVolumeSliderContainer);
            return settingsMenuPanel;
        }

        private void OnMasterVolumeChanged(ChangeEvent<float> evt)
        {
            masterVolumePref = Mathf.Clamp01(evt.newValue);
            PlayerPrefs.SetFloat(nameof(masterVolumePref), masterVolumePref);
            masterVolumeChanged?.Invoke(masterVolumePref);
        }

        private void OnMusicVolumeChanged(ChangeEvent<float> evt)
        {
            musicVolumePref = Mathf.Clamp01(evt.newValue);
            PlayerPrefs.SetFloat(nameof(musicVolumePref), musicVolumePref);
            musicVolumeChanged?.Invoke(musicVolumePref);
        }

        private void OnEffectsVolumeChanged(ChangeEvent<float> evt)
        {
            effectsVolumePref = Mathf.Clamp01(evt.newValue);
            PlayerPrefs.SetFloat(nameof(effectsVolumePref), effectsVolumePref);
            effectsVolumeChanged?.Invoke(effectsVolumePref);
        }
    }
}