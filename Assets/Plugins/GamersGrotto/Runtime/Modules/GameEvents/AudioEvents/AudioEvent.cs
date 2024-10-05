using UnityEngine;

namespace GamersGrotto.Runtime.Modules.GameEvents.AudioEvents
{
    [CreateAssetMenu(fileName = "New Audio Event", menuName = "GamersGrotto/Scriptable Audio System/Audio Event")]
    public class AudioEvent : ScriptableObject
    {
        public AudioClip clip;
        [Range(0,2)]public float volume = 1.0f;

        public void Play(AudioSource audioSource, bool loop)
        {
            if (clip == null || audioSource == null)
                return;

            audioSource.clip = clip;
            audioSource.time = 0;
            audioSource.volume = volume;
            audioSource.loop =  loop;
            audioSource.Play();
        }
        
        
    }
}