using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class Spawner : MonoBehaviour
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
        
        void Start()
        {
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
                spawnedObject.Spawn(); //Synchronizing the object with the network
                spawnedObjects.Add(spawnedObject);
                spawnedObject.TrySetParent(this.gameObject);
            }
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
