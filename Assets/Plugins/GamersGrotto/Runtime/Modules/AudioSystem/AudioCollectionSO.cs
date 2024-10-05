using GamersGrotto.Runtime.Modules.GameEvents.AudioEvents;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollectionSO", menuName = "Scriptable Objects/AudioCollectionSO")]
public class AudioCollectionSO : ScriptableObject {
    [SerializeField] AudioEvent[] audioClips;

    public AudioEvent[] AudioClips => audioClips;


    public AudioEvent GetRandomAudioEvent() {
        Debug.Assert(AudioClips.Length > 0, "AudioCollection is empty");
        return AudioClips[Random.Range(0, AudioClips.Length)];
    }
}