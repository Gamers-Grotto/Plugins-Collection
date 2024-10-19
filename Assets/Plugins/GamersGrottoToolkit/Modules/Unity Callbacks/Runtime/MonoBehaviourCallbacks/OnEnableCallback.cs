using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Unity_Callbacks.MonoBehaviourCallbacks
{
    public class OnEnableCallback : MonoBehaviour
    {
        public UnityEvent onEnable;

        private void OnEnable() => onEnable?.Invoke();
    }
}