using System.Collections.Generic;
using Gamersgrotto.Audio_system.Plugins.AudioEvents;
using GamersGrotto.Runtime.Modules.GameEvents.AudioEvents;
using UnityEngine;

namespace Gamersgrotto.Audio_system.Plugins {
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = "GamersGrotto/Audio System/AudioLibrarySO")]
    public class AudioLibrary : ScriptableObject
    {	
        [SerializeField] private List<AudioCollectionSO> audioCollections;
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
