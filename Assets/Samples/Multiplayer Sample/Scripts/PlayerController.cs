using System;
using GamersGrotto.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamersGrotto.Multiplayer_Sample
{
    public class PlayerController : NetworkBehaviour
    {
        public float sprintSpeed = 10f; 
        public float walkSpeed = 5f;
        public float rayCastDistance = 50f;
        public float rotationSpeed = 90f;
        public LayerMask groundLayer;
        
        private Camera playerCamera;
        private Vector2 input;
        private bool isSprinting;
        private Vector3? mouseWorldPosition;

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void OnSceneChanged(Scene previousScene, Scene newScene)
        {
            GetComponent<Rigidbody>().useGravity = true;
            EnableCamera();
        }

        private void Update()
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            isSprinting = Input.GetKey(KeyCode.LeftShift);
            
            if(playerCamera == null)
                return;
            
            var ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, rayCastDistance, groundLayer))
            {
                mouseWorldPosition = hit.point;
            }
            else mouseWorldPosition = null;
        }

        private void FixedUpdate()
        {
            if (!IsOwner)
                return;

            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            var movement = new Vector3(input.x, 0f, input.y).normalized * (isSprinting ? sprintSpeed : walkSpeed);
            transform.position = Vector3.Lerp(transform.position, transform.position + movement, Time.fixedDeltaTime);
        }

        private void HandleRotation()
        {
            if (mouseWorldPosition.HasValue)
            {
                var desiredRotation = Quaternion.LookRotation(transform.position.To(mouseWorldPosition.Value).WithY(0f), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, Time.fixedDeltaTime * rotationSpeed);
            }
        }

        private void OnServerInitialized() { }

        protected override void OnNetworkPreSpawn(ref NetworkManager networkManager) { }

        public override void OnNetworkSpawn()
        {
            enabled = IsOwner;
           
            if (IsOwner && !IsHost) {
                EnableCamera();
                GetComponent<Rigidbody>().useGravity = true;
            }
            gameObject.name = $"Player {OwnerClientId}";
            Debug.Log(gameObject.name + " spawned");
        }

        public override void OnNetworkDespawn() { }

        public void EnableCamera() {
            var cam = Camera.main;
            
            if(cam != null)
            {
                if(cam.TryGetComponent<CameraController>(out var cameraController))
                {
                    playerCamera = cam;
                    cameraController.target = transform;
                } else Debug.LogWarning("Camera Controller not found on MainCam");
            } else Debug.LogWarning($"Main Camera not found in Scene {SceneManager.GetActiveScene().name}");
        }
    }
}