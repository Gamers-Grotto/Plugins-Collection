﻿using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class CombatController : NetworkBehaviour
    {
        public GameObject fireballPrefab;
        public Transform fireballSpawnPoint;
        public float fireballSpeed = 10f;

        public override void OnNetworkSpawn()
        {
            enabled = IsOwner;
        }

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
        private void RequestFireballServerRpc(ServerRpcParams serverRpcParams = default)
        {
            var fireballInstance = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
            var networkObject = fireballInstance.GetComponent<NetworkObject>();

            if (networkObject != null)
            {
                networkObject.SpawnWithOwnership(serverRpcParams.Receive.SenderClientId,true);
                fireballInstance.GetComponent<Rigidbody>().linearVelocity = fireballSpawnPoint.forward * fireballSpeed;
            }
        }
    }
}