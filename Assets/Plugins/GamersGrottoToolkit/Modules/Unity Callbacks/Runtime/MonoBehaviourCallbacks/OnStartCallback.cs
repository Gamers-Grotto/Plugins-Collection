using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Unity_Callbacks.MonoBehaviourCallbacks
{
    public class OnStartCallback : MonoBehaviour
    {
        public UnityEvent onStart;

        private void Start() => onStart?.Invoke();
    }
}