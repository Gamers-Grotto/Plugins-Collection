using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.UnityCallbacks.MonoBehaviourCallbacks
{
    public class OnDisableCallback : MonoBehaviour
    {
        public UnityEvent onDisable;

        private void OnDisable() => onDisable?.Invoke();
    }
}