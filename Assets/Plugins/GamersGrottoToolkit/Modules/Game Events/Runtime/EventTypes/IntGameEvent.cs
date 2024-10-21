using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "Int Game Event", menuName = Constants.GameEventPath + "Int Game Event", order = 1)]
    public class IntGameEvent : GameEvent<int> { }
}