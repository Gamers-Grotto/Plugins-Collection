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

        else if (IsOwner)
        {
            Debug.Log("Client requesting player spawn...");
            RequestSpawnPlayerServerRpc();
        }
    }

    public override void OnNetworkDespawn()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unregister scene loaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[PlayerConnection] Scene {scene.name} loaded for {NetworkManager.Singleton.LocalClientId}, IsOwner: {IsOwner}, IsServer: {IsServer}");

        if (scene.buildIndex == 1) // Only spawn players in the game scene
        {
            if (IsServer)
            {
                Debug.Log("Server spawning player...");
                SpawnPlayer(NetworkManager.Singleton.LocalClientId);
            }
        }
    }

    private void SpawnPlayer(ulong clientId)
    {
        Debug.Log($"Spawning player for Client {clientId}");

        if (IsServer)
        {
            playerObjectInstance = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

            // Parent to this PlayerConnection object
            playerObjectInstance.transform.SetParent(transform);

            NetworkObject netObj = playerObjectInstance.GetComponent<NetworkObject>();
            netObj.SpawnWithOwnership(clientId);
            playerObjectInstance.TrySetParent(this.gameObject, true);
            
            //RPC to the client to enable camera
            
            EnableCameraClientRpc(clientId);
            Debug.Log($"Player spawned and parented to {gameObject.name}");
        }
    }
    
    [ClientRpc]
    void EnableCameraClientRpc(ulong clientId)
    {
        if (clientId == NetworkManager.Singleton.LocalClientId)
        {
            GetComponentInChildren<PlayerController>()?.EnableCamera();
        }
    }


    [ServerRpc(RequireOwnership = false)]
    private void RequestSpawnPlayerServerRpc(ServerRpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;

        Debug.Log($"[Server] Received spawn request from Client {clientId}");

        if (IsServer)
        {
            SpawnPlayer(clientId);
        }
    }
}
