using System.Collections.Generic;
using Gamersgrotto.Audio_System;
using GamersGrotto.Audio_System.AudioEvents;
using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Audio_System {
    [CreateAssetMenu(fileName = "AudioLibrary", menuName = Constants.AudioSystemPath + "AudioLibrarySO")]
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
