using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Unity_Callbacks.MonoBehaviourCallbacks
{
    public class OnAwakeCallback : MonoBehaviour
    {
        public UnityEvent onAwake;

        private void Awake() => onAwake?.Invoke();
    }
}