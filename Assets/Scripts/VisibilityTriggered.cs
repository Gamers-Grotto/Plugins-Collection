using System;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto
{
    [RequireComponent(typeof(Renderer))]
    public class VisibilityTriggered : MonoBehaviour
    {
        public UnityEvent onVisible;
        public UnityEvent onInvisible;

        private void OnBecameVisible() => onVisible?.Invoke();

        private void OnBecameInvisible() => onInvisible?.Invoke();
    }
}