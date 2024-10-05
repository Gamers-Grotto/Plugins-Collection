using UnityEngine;

namespace GamersGrotto.Runtime.Modules.ScriptableAudioSystem
{
    [CreateAssetMenu(fileName = "New Audio Event", menuName = "GamersGrotto/Scriptable Audio System/Audio Event")]
    public class AudioEvent : ScriptableObject
    {
        public AudioClip clip;
        public float volume = 1.0f;
        public bool loop = false;

        public void Play(AudioSource audioSource)
        {
            if (clip == null || audioSource == null) 
                return;
        
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
        }
        
        public static void PlayAudioEvent(AudioEvent audioEvent, AudioSource audioSource) => audioEvent?.Play(audioSource);
    }
}