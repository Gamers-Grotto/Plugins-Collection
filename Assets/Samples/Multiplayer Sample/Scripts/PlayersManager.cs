using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Multiplayer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GamersGrotto.Multiplayer_Sample {
    [GenerateSerializationForType(typeof(PlayerSessionData))]
    public struct PlayerSessionData : IEquatable<PlayerSessionData>, INetworkSerializeByMemcpy {
        public ulong clientId;
        public FixedString32Bytes playerName;

        public bool Equals(PlayerSessionData other) =>
            clientId == other.clientId && playerName.Equals(other.playerName);

        public override bool Equals(object obj) => obj is PlayerSessionData other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(clientId, playerName);
    }

    public class PlayersManager : NetworkBehaviour {
        public NetworkList<PlayerSessionData> playerData = new();
        public UnityEvent<List<PlayerSessionData>> OnPlayerDataChangedEvent;

        public PlayerSessionData LocalPlayerSessionData;
        
        public const string PLAYER_NAME_PROPERTY_KEY = "playerName";
        public const string PLAYER_ID_PROPERTY_KEY = "playerId";
        public string GetLocalPlayerName() => LocalPlayerSessionData.playerName.ToString();
        public string GetPlayerName(ulong clientId) => GetPlayerSessionData(clientId).playerName.ToString();
        public new bool IsLocalPlayer(ulong clientId) => clientId == NetworkManager.LocalClientId;
        
        public static PlayersManager Instance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Start() {
            playerData.OnListChanged += OnPlayerDataChanged;
            SceneManager.activeSceneChanged += OnSceneChanged;

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }

        void OnDisable() {
            playerData.OnListChanged -= OnPlayerDataChanged;
            SceneManager.activeSceneChanged -= OnSceneChanged;

            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
        }

        private void OnSceneChanged(Scene arg0, Scene arg1) {
            if (!IsHost)
                return;

            OnPlayerDataChangedEvent?.Invoke(playerData.AsList());
        }

        void OnClientConnected(ulong clientId) {
            Debug.Log("Client Connected: " + LocalPlayerSessionData.playerName + $"(ClientId: {clientId})");
            if (!IsLocalPlayer(clientId)) return;

            LocalPlayerSessionData.clientId = clientId;

            AddPlayerSessionDataServerRpc(LocalPlayerSessionData);
        }


        void OnClientDisconnected(ulong clientId) {
            Debug.Log("Client Disconnected: " + LocalPlayerSessionData.playerName + $"(ClientId: {clientId})");
            if (!IsHost) return;
            playerData.Remove(GetPlayerSessionData(clientId));
        }

        void OnPlayerDataChanged(NetworkListEvent<PlayerSessionData> changeevent) {
            OnPlayerDataChangedEvent?.Invoke(playerData.AsList());
        }

        /// <summary>
        /// This method gets the persistent player properties that are stored in the Unity Gaming Services backend.
        /// This is different from the client data which is only relevant for the current session.
        /// </summary>
        /// <returns></returns>
        internal static async Task<Dictionary<string, PlayerProperty>> GetPlayerProperties() {
            var playerName = await AuthenticationService.Instance.GetPlayerNameAsync();
            var playerId = AuthenticationService.Instance.PlayerId;

            var playerNameProperty = new PlayerProperty(playerName, VisibilityPropertyOptions.Member);
            var playerIdProperty = new PlayerProperty(playerId, VisibilityPropertyOptions.Member);

            var playerProperties = new Dictionary<string, PlayerProperty> {
                { PLAYER_NAME_PROPERTY_KEY, playerNameProperty },
                { PLAYER_ID_PROPERTY_KEY, playerIdProperty }
            };

            return playerProperties;
        }

       

        public PlayerSessionData GetPlayerSessionData(ulong clientId) {
            return playerData.AsList().FirstOrDefault(p => p.clientId == clientId);
        }

        public async Task SetPlayerName(string name) {
            if (!PlayerNameRequirementsMet(name)) {
                Debug.LogError("Player name needs to be min 2 characters");
                return;
            }

            if (LocalPlayerSessionData.playerName.Equals(new FixedString32Bytes(name))) {
                Debug.Log("Player name is already set to " + LocalPlayerSessionData.playerName);
                return;
            }

            LocalPlayerSessionData.playerName = name;
            await AuthenticationService.Instance.UpdatePlayerNameAsync(LocalPlayerSessionData.playerName.ToString());
        }

        [ServerRpc(RequireOwnership = false)]
        public void AddPlayerSessionDataServerRpc(PlayerSessionData playerSessionData) {
            if (playerData.AsList().Any(p => p.clientId == playerSessionData.clientId)) return;

            if (!PlayerNameRequirementsMet(playerSessionData.playerName.ToString())) return;

            playerData.Add(playerSessionData);
        }

        public static bool PlayerNameRequirementsMet(string playerName) =>
            !string.IsNullOrEmpty(playerName) && !string.IsNullOrWhiteSpace(playerName) &&
            playerName.Length >= 2;
    }
}