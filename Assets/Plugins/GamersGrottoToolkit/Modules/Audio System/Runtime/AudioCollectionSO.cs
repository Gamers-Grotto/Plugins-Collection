using GamersGrotto.Audio_System.AudioEvents;
using GamersGrotto.Core;
using UnityEngine;

namespace Gamersgrotto.Audio_System {
    [CreateAssetMenu(fileName = "AudioCollectionSO", menuName = Constants.AudioSystemPath + "AudioCollectionSO")]
    public class AudioCollectionSO : ScriptableObject {
        [SerializeField] AudioEvent[] audioClips;

        public AudioEvent[] AudioClips => audioClips;


        public AudioEvent GetRandomAudioEvent() {
            Debug.Assert(AudioClips.Length > 0, "AudioCollection is empty");
            return AudioClips[Random.Range(0, AudioClips.Length)];
        }
    }
}