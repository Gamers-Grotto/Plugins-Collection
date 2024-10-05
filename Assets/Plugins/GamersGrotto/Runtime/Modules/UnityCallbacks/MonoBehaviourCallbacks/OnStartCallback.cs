using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.UnityCallbacks.MonoBehaviourCallbacks
{
    public class OnStartCallback : MonoBehaviour
    {
        public UnityEvent onStart;

        private void Start() => onStart?.Invoke();
    }
}