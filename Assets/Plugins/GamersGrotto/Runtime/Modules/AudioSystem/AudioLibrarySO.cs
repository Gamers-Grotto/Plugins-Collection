using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Scriptable Objects/AudioLibrarySO")]
public class AudioLibrary : ScriptableObject
{	
 [SerializeField] private List<AudioCollectionSO> audioCollections;
 public AudioCollectionSO GetRandomCollection()
 {
    return audioCollections[Random.Range(0, audioCollections.Count)];
 }
  public AudioClip GetRandomClip()
  {
    return audioCollections[Random.Range(0, audioCollections.Count)].GetRandomClip();
  }
}
