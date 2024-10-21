using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Audio_System.AudioEvents
{
    [CreateAssetMenu(fileName = "New Audio Event", menuName = Constants.AudioSystemPath + "Audio Event")]
    public class AudioEvent : ScriptableObject
    {
        public AudioClip clip;
        [Range(0,1)]public float volume = 1.0f;
        [Range(-3,3)]public float pitch = 1.0f;

        public void Play(AudioSource audioSource, bool loop)
        {
            if (clip == null || audioSource == null)
                return;

            audioSource.clip = clip;
            audioSource.time = 0;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.loop =  loop;
            audioSource.Play();
        }
    }
}