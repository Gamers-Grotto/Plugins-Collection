using Gamersgrotto.Audio_system.Plugins.AudioEvents;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.GameEvents.AudioEvents.Sample.Scripts
{
    public class PlayAudioEventOnStart : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioEvent audioEvent;
        private void Start()
        {
            if(!audioSource || !audioEvent)
                return;
            
            audioEvent.Play(audioSource, false);
        }
    }
}