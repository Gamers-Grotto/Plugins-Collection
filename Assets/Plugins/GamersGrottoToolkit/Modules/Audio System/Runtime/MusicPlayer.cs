using System.Collections;
using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Audio_System
{
    public class MusicPlayer : MonoBehaviour
    {
        public bool playOnStart = true;
        public AudioSource musicAudioSource; 
        [ShowInInspector] public AudioCollectionSO musicCollection;

        private Coroutine endlessMusicRoutine;
        
        private void Start()
        {
            if (playOnStart)
                endlessMusicRoutine = StartCoroutine(PlayAllMusicConsecutively());
        }

        private IEnumerator PlayAllMusicConsecutively()
        {
            while (true) 
            {
                foreach (var audioEvent in musicCollection.AudioClips) 
                {
                    audioEvent.Play(musicAudioSource);
                    yield return new WaitForSeconds(audioEvent.clip.length);
                }
            }
        }

        [Button]
        public void PlayMusic()
        {
            if(musicAudioSource.isPlaying)
                musicAudioSource.Stop();
            
            if(endlessMusicRoutine != null)
                StopCoroutine(endlessMusicRoutine);

            endlessMusicRoutine = StartCoroutine(PlayAllMusicConsecutively());
        }

        [Button]
        public void StopMusic()
        {
            musicAudioSource.Stop();
            
            if(endlessMusicRoutine != null)
                StopCoroutine(endlessMusicRoutine);
        }
    }
}