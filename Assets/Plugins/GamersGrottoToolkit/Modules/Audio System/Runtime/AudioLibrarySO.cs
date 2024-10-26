using System.Collections.Generic;
using GamersGrotto.Audio_System.AudioEvents;
using GamersGrotto.Core;
using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Audio_System {
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = Constants.AudioSystemPath + "AudioLibrarySO")]
    public class AudioLibrary : ScriptableObject
    {	
        [SerializeField, ShowInInspector] private List<AudioCollectionSO> audioCollections;
        public AudioCollectionSO GetRandomCollection()
        {
            return audioCollections[Random.Range(0, audioCollections.Count)];
        }
        public AudioEvent GetRandomAudioEvent()
        {
            return audioCollections[Random.Range(0, audioCollections.Count)].GetRandomAudioEvent();
        }
    }
}