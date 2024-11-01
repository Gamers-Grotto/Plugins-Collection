using GamersGrotto.Game_Events;
using UnityEngine;

public class SquashCheck : MonoBehaviour
{
    private GroundCheck groundCheck;

    [SerializeField] private GameEvent safeResetGameEvent;

    private void Awake() => groundCheck = GetComponent<GroundCheck>();

    public void Foo()
    {
        if (!groundCheck.IsGrounded) 
            return;
            
        safeResetGameEvent?.Raise();
    }
}