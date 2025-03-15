using GamersGrotto.Multiplayer_Sample;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerConnection : NetworkBehaviour
{
    public static PlayerConnection LocalInstance { get; private set; }
    public NetworkObject playerPrefab; // Reference to the player prefab
    private NetworkObject playerObjectInstance;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            LocalInstance = this;
           SceneManager.sceneLoaded += OnSceneLoaded; // Register scene loaded event
        }
        else if(IsOwner){
            RequestSpawnPlayerServerRpc();
        }
        
    }

    public override void OnNetworkDespawn()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister scene loaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene {scene.name} loaded for {NetworkManager.Singleton.LocalClientId} (Owner: {IsOwner})");

        if (scene.buildIndex == 1)
        {
            if (IsServer)
            {
                Debug.Log("Server spawning player...");
                SpawnPlayer();
            }

            if (IsOwner) {
                GetComponentInChildren<PlayerController>().EnableCamera();
            }
           
        }
    }

    private void SpawnPlayer()
    {
        if (IsServer)
        {
            playerObjectInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            playerObjectInstance.GetComponent<NetworkObject>().SpawnWithOwnership(NetworkManager.Singleton.LocalClientId);
            playerObjectInstance.GetComponent<PlayerController>().EnableCamera();

        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestSpawnPlayerServerRpc(ServerRpcParams rpcParams = default)
    {

        if (IsServer)
        {
            NetworkObject playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            playerObject.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);

        }
    }
}
