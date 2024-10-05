using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.UnityCallbacks.MonoBehaviourCallbacks
{
    public class OnDestroyCallback : MonoBehaviour
    {
        public UnityEvent onDestroy;

        private void OnDestroy() => onDestroy?.Invoke();
    }
}