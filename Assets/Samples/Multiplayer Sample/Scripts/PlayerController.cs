using System;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerController : NetworkBehaviour
    {
        public float sprintSpeed = 10f;  // Sprint speed
        public float walkSpeed = 5f;    // Normal walking speed
        [SerializeField] Camera playerCamera;
        
        private Vector2 input;
        
        private void FixedUpdate()
        {
            if (!IsOwner)
                return;

            // Get input for movement
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Determine if sprinting
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

            // Calculate movement direction and speed
            var movement = new Vector3(input.x, 0f, input.y).normalized * currentSpeed;

            // Apply movement smoothly
            transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.fixedDeltaTime);
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