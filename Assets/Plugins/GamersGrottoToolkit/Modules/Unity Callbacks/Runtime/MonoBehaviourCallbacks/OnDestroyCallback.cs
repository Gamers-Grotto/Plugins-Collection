using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Unity_Callbacks.MonoBehaviourCallbacks
{
    public class OnDestroyCallback : MonoBehaviour
    {
        public UnityEvent onDestroy;

        private void OnDestroy() => onDestroy?.Invoke();
    }
}