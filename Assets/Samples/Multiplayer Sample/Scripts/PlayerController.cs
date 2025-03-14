﻿using System;
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
            
            Debug.Log(gameObject.name + " spawned");
            if(gameObject.GetComponent<NetworkObject>().IsLocalPlayer)
                playerCamera.gameObject.SetActive(true);
        }

        public override void OnNetworkDespawn()
        {
            Debug.Log(gameObject.name + " despawned");
        }
        
    }
}