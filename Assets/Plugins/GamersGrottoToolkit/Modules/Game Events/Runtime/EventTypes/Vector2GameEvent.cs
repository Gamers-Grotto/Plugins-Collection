using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "Vector2 Game Event", menuName = Constants.GameEventPath + "Vector2 Game Event", order = 5)]
    public class Vector2GameEvent : GameEvent<Vector2> { }
}