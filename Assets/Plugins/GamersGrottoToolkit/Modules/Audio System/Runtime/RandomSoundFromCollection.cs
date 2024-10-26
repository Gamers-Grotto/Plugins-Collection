using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Audio_System
{
    public class RandomSoundFromCollection : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioCollectionSO audioCollection;

        [Button]
        public void Play()
        {
            audioCollection.GetRandomAudioEvent().Play(audioSource);
        }
    }
}