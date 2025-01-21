using UnityEngine;

public class Spike : CollisionDamageDealer
{
    public override void DoChildLogic(Collision other) {
        base.DoChildLogic(other);
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<Locomotion>().InterruptDash();
            other.gameObject.GetComponent<SafeReset>().ToLastGroundedPosition();
        }
    }
}