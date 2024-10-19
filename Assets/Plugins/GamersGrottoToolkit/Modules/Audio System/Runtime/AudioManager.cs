using System.Collections;
using Gamersgrotto.Audio_system.Plugins.AudioEvents;
using GamersGrotto.Core;
using GamersGrotto.Runtime.Modules.GameEvents.AudioEvents;
using UnityEngine;

namespace Gamersgrotto.Audio_system.Plugins {
    public class AudioManager : Singleton<AudioManager> {
        public AudioSource uiSoundPlayer;
        public AudioSource musicPlayer;
        [SerializeField] AudioSettings audioSettings;
        private short uiSoundIndex;

        public static void PlayAudioEvent(AudioEvent audioEvent, AudioSource audioSource, bool loop = false) =>
            audioEvent?.Play(audioSource, loop);


        public void Start() {
            audioSettings.Initialize();
        }

        #region UI

        public void PlayUISound(AudioEvent audioEvent) {
            audioEvent?.Play(uiSoundPlayer, false);
        }

        /// <summary>
        /// This method plays the sound from the AudioCollectionSO in order, looping when it reaches the end of the collection
        /// Optional way to play UI sounds, see Crusader Kings 2 country selection sounds for an example
        /// </summary>
        /// <param name="audioCollection"></param>
        public void PlayUISound(AudioCollectionSO audioCollection) {
            if (audioCollection.AudioClips.Length == 0) {
                Debug.LogWarning("AudioCollection is empty");
                return;
            }

            PlayUISound(audioCollection.AudioClips[uiSoundIndex]);
            uiSoundIndex = (short)((uiSoundIndex + 1) % audioCollection.AudioClips.Length);
        }


        public void PlayRandomUISound(AudioCollectionSO audioCollection) {
            PlayUISound(audioCollection.GetRandomAudioEvent());
        }

        #endregion

        #region Music

        public void PlayMusic(AudioEvent audioEvent) {
            audioEvent?.Play(musicPlayer, false);
        }

        /// <summary>
        /// Plays the music from the AudioCollectionSO in order
        /// Loops when it reaches the end of the collection
        /// </summary>
        /// <param name="audioCollection"></param>
        public void PlayMusic(AudioCollectionSO audioCollection) {
            StopCoroutine(nameof(PlayMusicRoutine));
            if (audioCollection.AudioClips.Length == 0) {
                Debug.LogWarning("AudioCollection is empty");
                return;
            }

            StartCoroutine(PlayMusicRoutine(audioCollection));
        }

        private IEnumerator PlayMusicRoutine(AudioCollectionSO audioCollection) {
            while (true) {
                foreach (var audioEvent in audioCollection.AudioClips) {
                    PlayMusic(audioEvent);
                    yield return new WaitForSeconds(audioEvent.clip.length);
                }
            }
        }

        #endregion
    }
}