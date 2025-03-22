using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class CombatController : NetworkBehaviour
    {
        public GameObject fireballPrefab;
        public Transform fireballSpawnPoint;
        public float fireballSpeed = 10f;

        private void Update()
        {
            if (!IsOwner)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                RequestFireballServerRpc();
            }
        }
        
        [ServerRpc]
        private void RequestFireballServerRpc()
        {
            var fireballInstance = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
            var networkObject = fireballInstance.GetComponent<NetworkObject>();

            if (networkObject != null)
            {
                networkObject.Spawn(true);
                fireballInstance.GetComponent<Rigidbody>().linearVelocity = fireballSpawnPoint.forward * fireballSpeed;
            }
        }
    }
}