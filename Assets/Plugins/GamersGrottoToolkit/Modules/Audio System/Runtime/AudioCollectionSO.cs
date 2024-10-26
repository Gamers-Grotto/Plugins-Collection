using UnityEngine;
using GamersGrotto.Core;
using GamersGrotto.Audio_System.AudioEvents;
using GamersGrotto.Core.Extended_Attributes;

namespace GamersGrotto.Audio_System 
{
    [CreateAssetMenu(fileName = "AudioCollectionSO", menuName = Constants.AudioSystemPath + "Audio Collection")]
    public class AudioCollectionSO : ScriptableObject 
    {
        [SerializeField, ShowInInspector] private AudioEvent[] audioClips;

        public AudioEvent[] AudioClips => audioClips;
        
        public AudioEvent GetRandomAudioEvent()
        {
            Debug.Assert(AudioClips.Length > 0, "AudioCollection is empty");
            return AudioClips[Random.Range(0, AudioClips.Length)];
        }
    }
}