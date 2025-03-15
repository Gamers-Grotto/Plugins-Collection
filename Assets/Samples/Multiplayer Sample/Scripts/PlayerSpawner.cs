using System;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private NetworkObject playerPrefab;
        [SerializeField] private Transform spawnPoint;


        void Awake() {
            SpawnPlayer();
        }

        void SpawnPlayer() {
            //client id
            
            NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(playerPrefab,NetworkManager.Singleton.LocalClientId,
                true,true, false, spawnPoint.position, Quaternion.identity);
        }
    }
}
