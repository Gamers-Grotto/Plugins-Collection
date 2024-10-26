using GamersGrotto.Audio_System.AudioEvents;
using UnityEngine;

namespace GamersGrotto.Audio_System
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;

        public void PlayAudioEvent(AudioEvent audioEvent)
        {
            audioEvent.Play(audioSource);
        }
    }
}