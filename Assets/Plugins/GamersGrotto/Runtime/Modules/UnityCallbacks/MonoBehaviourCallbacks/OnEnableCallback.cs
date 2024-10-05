using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.UnityCallbacks.MonoBehaviourCallbacks
{
    public class OnEnableCallback : MonoBehaviour
    {
        public UnityEvent onEnable;

        private void OnEnable() => onEnable?.Invoke();
    }
}