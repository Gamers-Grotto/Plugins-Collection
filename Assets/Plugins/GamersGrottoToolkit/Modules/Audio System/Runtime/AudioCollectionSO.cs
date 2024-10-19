using Gamersgrotto.Audio_system.Plugins.AudioEvents;
using GamersGrotto.Runtime.Modules.GameEvents.AudioEvents;
using UnityEngine;

namespace Gamersgrotto.Audio_system.Plugins {
    [CreateAssetMenu(fileName = "AudioCollectionSO", menuName = "GamersGrotto/Audio System/AudioCollectionSO")]
    public class AudioCollectionSO : ScriptableObject {
        [SerializeField] AudioEvent[] audioClips;

        public AudioEvent[] AudioClips => audioClips;


        public AudioEvent GetRandomAudioEvent() {
            Debug.Assert(AudioClips.Length > 0, "AudioCollection is empty");
            return AudioClips[Random.Range(0, AudioClips.Length)];
        }
    }
}