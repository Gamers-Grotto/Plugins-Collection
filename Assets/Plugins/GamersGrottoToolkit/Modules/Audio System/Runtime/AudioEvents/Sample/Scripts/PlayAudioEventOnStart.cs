﻿using UnityEngine;

namespace GamersGrotto.Audio_System.AudioEvents.Sample.Scripts
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