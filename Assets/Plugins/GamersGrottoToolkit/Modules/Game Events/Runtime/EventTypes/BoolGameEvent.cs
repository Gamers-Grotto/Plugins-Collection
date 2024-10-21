using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "Bool Game Event", menuName = Constants.GameEventPath + "Bool Game Event", order = 3)]
    public class BoolGameEvent : GameEvent<bool> { }
}