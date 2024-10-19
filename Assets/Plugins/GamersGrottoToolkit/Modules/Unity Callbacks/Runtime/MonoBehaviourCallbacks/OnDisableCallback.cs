using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Unity_Callbacks.MonoBehaviourCallbacks
{
    public class OnDisableCallback : MonoBehaviour
    {
        public UnityEvent onDisable;

        private void OnDisable() => onDisable?.Invoke();
    }
}