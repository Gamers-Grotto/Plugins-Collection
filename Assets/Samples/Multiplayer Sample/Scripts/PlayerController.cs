using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private float moveSpeed = 4f;
        [SerializeField] Camera playerCamera;
        
        private Vector2 input;
        
        private void Update()
        {
            if(!IsOwner)
                return;
            
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            var movement = new Vector3(input.x, 0f, input.y) * moveSpeed;
            transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.deltaTime);
        }

        private void OnServerInitialized()
        {
            Debug.Log("OnServerInitialized");
        }

        protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
        {
            Debug.Log("OnNetworkPreSpawn");
        }

        public override void OnNetworkSpawn()
        {
            gameObject.name = $"Player {OwnerClientId}";
            SetPlayerNameClientRpc();
            Debug.Log(gameObject.name + " spawned");
        }

        public override void OnNetworkDespawn()
        {
            Debug.Log(gameObject.name + " despawned");
        }

        public void EnableCamera() {
            playerCamera.gameObject.SetActive(true);
        }

        [ClientRpc]
        public void SetPlayerNameClientRpc()
        {
            var players = SessionManager.Instance.ActiveSession.Players;

            foreach (var player in players)
            {
                player.Properties.TryGetValue(SessionManager.PLAYER_NAME_PROPERTY_KEY, out var playerName);
                Debug.Log($"Player : {player.Id} name : {playerName}");
                
                if(player.Id == OwnerClientId.ToString())
                    GetComponentInChildren<PlayerWorldSpaceUI>().SetPlayerName(player.Properties[SessionManager.PLAYER_NAME_PROPERTY_KEY].Value);
            }
        }
    }
}