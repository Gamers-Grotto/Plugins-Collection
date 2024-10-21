using System;
using Codice.CM.Common;
using UnityEngine;
using Constants = GamersGrotto.Core.Constants;

namespace GamersGrotto.Game_Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = Constants.GameEventPath + "Game Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private event Action Listeners = delegate { };

        public void Raise()
        {
            Listeners.Invoke();
        }

        public void RegisterListener(Action listener)
        {
            Listeners += listener;
        }

        public void UnregisterListener(Action listener)
        {
            Listeners -= listener;
        }
    }

    public abstract class GameEvent<T> : ScriptableObject
    {
        private event Action<T> Listeners = delegate { };

        public void Raise(T item)
        {
            Listeners.Invoke(item);
        }

        public void RegisterListener(Action<T> listener)
        {
            Listeners += listener;
        }

        public void UnregisterListener(Action<T> listener)
        {
            Listeners -= listener;
        }
    }
}