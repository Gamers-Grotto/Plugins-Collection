using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "String Game Event", menuName = Constants.GameEventPath + "String Game Event", order = 4)]
    public class StringGameEvent : GameEvent<string> { }
}