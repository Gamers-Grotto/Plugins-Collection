using System.Collections.Generic;
using GamersGrotto.Runtime.Modules.GameEvents.AudioEvents;
using UnityEngine;

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
