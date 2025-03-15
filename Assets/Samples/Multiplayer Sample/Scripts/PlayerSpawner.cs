using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject playerPrefab;
        [SerializeField] private Transform spawnPoint;


        void Start() {
            SpawnPlayerServerRpc();
        }

        [ServerRpc]
        void SpawnPlayerServerRpc() {
            NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(playerPrefab,NetworkManager.Singleton.LocalClientId,
                true,true, false, spawnPoint.position, Quaternion.identity);
        }
    }
}
