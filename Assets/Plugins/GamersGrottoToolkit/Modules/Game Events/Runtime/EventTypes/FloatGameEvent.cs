using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "Float Game Event", menuName = Constants.GameEventPath + "Float Game Event", order = 2)]
    public class FloatGameEvent : GameEvent<float> { }
}