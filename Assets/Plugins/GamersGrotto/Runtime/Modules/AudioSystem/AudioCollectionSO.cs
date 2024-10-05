using UnityEngine;

[CreateAssetMenu(fileName = "AudioCollectionSO", menuName = "Scriptable Objects/AudioCollectionSO")]
public class AudioCollectionSO : ScriptableObject {
    [SerializeField] AudioClip[] audioClips;

    public AudioClip[] AudioClips => audioClips;


    public AudioClip GetRandomClip() {
        Debug.Assert(AudioClips.Length > 0, "AudioCollection is empty");
        return AudioClips[Random.Range(0, AudioClips.Length)];
    }
}