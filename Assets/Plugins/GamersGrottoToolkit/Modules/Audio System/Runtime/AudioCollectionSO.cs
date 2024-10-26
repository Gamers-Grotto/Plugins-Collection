using GamersGrotto.Audio_System.AudioEvents;
using GamersGrotto.Core;
using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Audio_System {
    [CreateAssetMenu(fileName = "AudioCollectionSO", menuName = Constants.AudioSystemPath + "AudioCollectionSO")]
    public class AudioCollectionSO : ScriptableObject {
        [SerializeField, ShowInInspector] AudioEvent[] audioClips;

        public AudioEvent[] AudioClips => audioClips;


        public AudioEvent GetRandomAudioEvent() {
            Debug.Assert(AudioClips.Length > 0, "AudioCollection is empty");
            return AudioClips[Random.Range(0, AudioClips.Length)];
        }
    }
}