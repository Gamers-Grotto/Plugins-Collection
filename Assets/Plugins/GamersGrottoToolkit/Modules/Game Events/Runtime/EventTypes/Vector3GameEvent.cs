using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "Vector3 Game Event", menuName = Constants.GameEventPath + "Vector3 Game Event", order = 6)]
    public class Vector3GameEvent : GameEvent<Vector3> { }
}