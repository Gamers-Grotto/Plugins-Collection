using GamersGrotto.Runtime.Core;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.ScriptableAudioSystem
{
    public class ScriptableAudioManager : Singleton<ScriptableAudioManager>
    {
        public void PlayAudioEvent(AudioEvent audioEvent, AudioSource audioSource) => audioEvent?.Play(audioSource);
    }
}