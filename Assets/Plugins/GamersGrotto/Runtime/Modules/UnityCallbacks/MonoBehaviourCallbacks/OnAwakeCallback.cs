using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.UnityCallbacks.MonoBehaviourCallbacks
{
    public class OnAwakeCallback : MonoBehaviour
    {
        public UnityEvent onAwake;

        private void Awake() => onAwake?.Invoke();
    }
}