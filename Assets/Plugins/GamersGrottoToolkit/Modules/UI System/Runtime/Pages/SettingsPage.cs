using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GamersGrotto.Ui_System.Pages
{
    public class SettingsPage : Page
    {
        [Header("Audio Settings")]
        public UnityEvent<float> masterVolumeChanged;
        public UnityEvent<float> musicVolumeChanged;
        public UnityEvent<float> effectsVolumeChanged;

        private float masterVolSetting;
        private float musicVolSetting;
        private float sfxVolSetting;

        protected override void Awake()
        {
            base.Awake();
            masterVolSetting = Mathf.Clamp(PlayerPrefs.GetFloat("masterVolume", 1f), 0.0001f, 1f);
            musicVolSetting = Mathf.Clamp(PlayerPrefs.GetFloat("musicVolume", 1f), 0.001f, 1f);
            sfxVolSetting = Mathf.Clamp(PlayerPrefs.GetFloat("sfxVolume", 1f), 0.001f, 1f);
        }

        protected override IEnumerator DrawUI(VisualElement root)
        {
            var settingsMenuPanel = CreateBox();
            settingsMenuPanel.style.minWidth = 600;
            
            var background = Create("full-screen");
            var settingMenuTitleLabel = CreateLabel("Settings", "menu-title");
            var audioSettingsPanel = CreateAudioSettingsPanel();
            
            settingsMenuPanel.Add(settingMenuTitleLabel);
            settingsMenuPanel.Add(audioSettingsPanel);
            background.Add(settingsMenuPanel);
            root.Add(background);
            
            #region Absolute
            var backButton = CreateButton("Back", () => UIManager.Instance.GotoMainMenuPage());
            backButton.AddToClassList("bottom-right-button");
            root.Add(backButton);
            #endregion
            
            yield break;
        }

        private Box CreateAudioSettingsPanel()
        {
            var audioSettingsContainer = CreateBox();
            audioSettingsContainer.style.width = new StyleLength(Length.Percent(100f));
            audioSettingsContainer.Add(CreateLabel("Volume Settings", "menu-title"));
            
            var masterSlider = CreateSlider(0.0001f, 1f, "Master");
            masterSlider.SetValueWithoutNotify(masterVolSetting);
            masterSlider.RegisterValueChangedCallback(OnMasterVolumeChanged);
            audioSettingsContainer.Add(masterSlider);
            
            var musicSlider = CreateSlider(0.0001f, 1f, "Music");
            musicSlider.SetValueWithoutNotify(musicVolSetting);
            musicSlider.RegisterValueChangedCallback(OnMusicVolumeChanged);
            audioSettingsContainer.Add(musicSlider);
            
            var effectsSlider = CreateSlider(0.0001f, 1f, "Effects");
            effectsSlider.SetValueWithoutNotify(sfxVolSetting);
            effectsSlider.RegisterValueChangedCallback(OnEffectsVolumeChanged);
            audioSettingsContainer.Add(effectsSlider);

            return audioSettingsContainer;
        }

        private void OnMasterVolumeChanged(ChangeEvent<float> evt)
        {
            masterVolSetting = evt.newValue;
            masterVolumeChanged?.Invoke(masterVolSetting);
        }

        private void OnMusicVolumeChanged(ChangeEvent<float> evt)
        {
            musicVolSetting = evt.newValue;
            musicVolumeChanged?.Invoke(musicVolSetting);
        }

        private void OnEffectsVolumeChanged(ChangeEvent<float> evt)
        {
            sfxVolSetting = evt.newValue;
            effectsVolumeChanged?.Invoke(sfxVolSetting);
        }
    }
}