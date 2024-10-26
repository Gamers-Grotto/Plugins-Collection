using System;
using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Audio_System
{
    public class AudioSystemInitializer : MonoBehaviour
    {
        [SerializeField, ShowInInspector] private AudioSettings audioSettings;

        private void Awake()
        {
            audioSettings?.Initialize();
        }
    }
}