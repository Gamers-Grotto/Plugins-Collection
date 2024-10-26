using GamersGrotto.Core;
using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Audio_System.AudioEvents
{
    [CreateAssetMenu(fileName = "New Audio Event", menuName = Constants.AudioSystemPath + "Audio Event")]
    public class AudioEvent : ScriptableObject
    {
        public AudioClip clip;
        [MinMaxRange(0.5f, 2f)] public RangedFloat volume;
        [MinMaxRange(0.1f, 2f)] public RangedFloat pitch;

        public void Play(AudioSource audioSource, bool loop = false)
        {
            if (clip == null || audioSource == null)
                return;

            audioSource.clip = clip;
            audioSource.time = 0;
            audioSource.volume = volume.Value;
            audioSource.pitch = pitch.Value;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
}