using UnityEngine;

namespace GamersGrotto.Runtime.Modules.GameEvents.EventTypes
{
    [CreateAssetMenu(fileName = "GameObject Game Event", menuName = "GamersGrotto/Game Events/GameObject Game Event", order = 3)]
    public class GameObjectGameEvent : GameEvent<GameObject> { }
}