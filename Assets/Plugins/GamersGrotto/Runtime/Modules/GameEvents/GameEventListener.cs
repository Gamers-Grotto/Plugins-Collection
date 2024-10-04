using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private UnityEvent response;

        private void OnEnable()
        {
            if (gameEvent != null)
            {
                gameEvent.RegisterListener(OnEventRaised);
            }
        }

        private void OnDisable()
        {
            if (gameEvent != null)
            {
                gameEvent.UnregisterListener(OnEventRaised);
            }
        }

        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }

    public abstract class GameEventListener<T> : MonoBehaviour
    {
        [SerializeField] private GameEvent<T> gameEvent;
        [SerializeField] private UnityEvent<T> response;

        private void OnEnable()
        {
            if (gameEvent != null)
            {
                gameEvent.RegisterListener(OnEventRaised);
            }
        }

        private void OnDisable()
        {
            if (gameEvent != null)
            {
                gameEvent.UnregisterListener(OnEventRaised);
            }
        }

        public void OnEventRaised(T item)
        {
            response?.Invoke(item);
        }
    }
}