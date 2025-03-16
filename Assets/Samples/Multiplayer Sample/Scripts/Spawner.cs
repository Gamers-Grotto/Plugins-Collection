using System;
using System.Collections.Generic;
using GamersGrotto.Core.Extended_Attributes;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamersGrotto.Multiplayer_Sample
{
    public class Spawner : NetworkBehaviour
    {
        [SerializeField] NetworkObject prefab;
        [SerializeField] int amount = 10;
        [SerializeField] bool spawnOnStart = true;
        [SerializeField] bool spawnOnPoints = false;
        [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();
        [SerializeField] bool spawnInArea = false;
        [SerializeField] Transform spawnAreaCenter;
        [SerializeField] Vector3 spawnAreaSize = new Vector3(1,1,1);
        
        [SerializeField] bool drawGizmos;
        
        List<NetworkObject> spawnedObjects = new List<NetworkObject>();

        void Awake() {
            if (NetworkManager.Singleton.IsServer) {
                GetComponent<NetworkObject>().Spawn();
            }
        }

        public override void OnNetworkSpawn() {
            base.OnNetworkSpawn();
            if (spawnOnStart && NetworkManager.Singleton.IsServer)
            {
                SpawnAllPrefabs();
            }
        }

        void SpawnAllPrefabs() {
            for (int i = 0; i < amount; i++)
            {
                var spawnPosition = Vector3.zero;
                
                if (spawnInArea)
                {
                    spawnPosition = new Vector3(
                        Random.Range(spawnAreaCenter.position.x - spawnAreaSize.x / 2, spawnAreaCenter.position.x + spawnAreaSize.x / 2),
                        0,
                        Random.Range(spawnAreaCenter.position.z - spawnAreaSize.z / 2, spawnAreaCenter.position.z + spawnAreaSize.z / 2)
                    );
                }
                else if (spawnOnPoints)
                {
                    spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position;
                }
                var spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                spawnedObject.Spawn(true); //Synchronizing the object with the network
                spawnedObjects.Add(spawnedObject);
                spawnedObject.TrySetParent(this.gameObject.GetComponent<NetworkObject>());
            }
        }

        [ServerRpc(RequireOwnership = false),ContextMenu("Request Spawn All Prefabs")]
        public void RequestSpawnAllPrefabsServerRpc() {
            SpawnAllPrefabs();
        }
        private void OnDrawGizmos()
        {
            if(!drawGizmos) return;
            if (spawnInArea)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(spawnAreaCenter.position, spawnAreaSize);
            }
            else if (spawnOnPoints)
            {
                Gizmos.color = Color.green;
                foreach (var point in spawnPoints)
                {
                    Gizmos.DrawWireSphere(point.transform.position, 0.5f);
                }
            }
        }
    }
}
