using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.Game_Events.EventTypes
{
    [CreateAssetMenu(fileName = "GameObject Game Event", menuName = Constants.GameEventPath + "GameObject Game Event", order = 7)]
    public class GameObjectGameEvent : GameEvent<GameObject> { }
}