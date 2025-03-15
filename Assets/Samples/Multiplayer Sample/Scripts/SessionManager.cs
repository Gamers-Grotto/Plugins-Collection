using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamersGrotto.Core.Extended_Attributes;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample {
    public class SessionManager : MonoBehaviour {
        public short maxPlayers = 8;
        public string sessionName = "Default Session";
        public string sessionPassword = null;
        string playerName;
        public static SessionManager Instance { get; private set; }
        NetworkManager networkManager;

        const string PLAYER_NAME_PROPERTY_KEY = "playerName";
        const string PLAYER_ID_PROPERTY_KEY = "playerId";
        ISession activeSession;
        
        public string PlayerName => playerName;

        ISession ActiveSession {
            get => activeSession;
            set {
                activeSession = value;
                Debug.Log($"Active session Id is now {activeSession?.Id}");
            }
        }

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void OnDisable() {
            UnregisterSessionEvents();
        }

        async void Start() {
            try {
                await UnityServices.InitializeAsync(); // Initialize Unity Gaming Services SDKs
                await AuthenticationService.Instance
                    .SignInAnonymouslyAsync(); // Can be replaced with any other sign-in method
                Debug.Log("Signed in anonymously. PlayerID: " + AuthenticationService.Instance.PlayerId);
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
        }

        #region Event Registration

        void RegisterSessionEvents() {
            ActiveSession.Changed += OnSessionChanged;
            ActiveSession.PlayerJoined += OnPlayerJoined;
            ActiveSession.PlayerLeaving += OnPlayerLeaving;
          
        }

        void UnregisterSessionEvents() {
            if(ActiveSession == null)
                return;
            
            ActiveSession.Changed -= OnSessionChanged;
            ActiveSession.PlayerJoined -= OnPlayerJoined;
            ActiveSession.PlayerLeaving -= OnPlayerLeaving;
        }

        void OnSessionChanged() {
            Debug.Log("Session changed. New session Id: " + ActiveSession.Id);
        }

        void OnPlayerJoined(string obj) {
            Debug.Log("Player joined: " + obj);
        }

        void OnPlayerLeaving(string obj) {
            Debug.Log("Player left: " + obj);
        }

        #endregion


        /// <summary>
        /// Custom game-specific properties that apply to an individual player.
        /// Examples, name, id, level, role etc.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, PlayerProperty>> GetPlayerProperties() {
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


        [Button]
        public async void StartSessionAsHost() {
            var playerProperties = await GetPlayerProperties();
            var options = new SessionOptions {
                Name = sessionName,
                MaxPlayers = maxPlayers,
                IsPrivate = false,
                IsLocked = false,
                PlayerProperties = playerProperties
            };
            if (sessionPassword is { Length: >= 8 }) {
                //Idk, arbitrary password requirement when set in body.
                options.Password = sessionPassword;
            }

            options.WithRelayNetwork();


            ActiveSession = await MultiplayerService.Instance.CreateSessionAsync(options);
            Debug.Log($"Session {ActiveSession.Name}({ActiveSession.Id}) created successfully! Join code: {ActiveSession.Code}");
            RegisterSessionEvents();

            await WorldSaveGameManager.Instance.LoadNewGame();
           
        }

        public async Task JoinSessionById(string sessionId) {
            ActiveSession = await MultiplayerService.Instance.JoinSessionByIdAsync(sessionId);
            Debug.Log($"Session {ActiveSession.Id} joined successfully!");

            await WorldSaveGameManager.Instance.LoadNewGame();
        }

        public async Task JoinSessionByCode(string sessionCode) {
            ActiveSession = await MultiplayerService.Instance.JoinSessionByCodeAsync(sessionCode);
            Debug.Log($"Session {ActiveSession.Id} joined successfully!");

            await WorldSaveGameManager.Instance.LoadNewGame();
        }

        public async Task KickPlayer(string playerId) {
            if (!ActiveSession.IsHost) return;

            await ActiveSession.AsHost().RemovePlayerAsync(playerId);
            Debug.Log($"Player {playerId} kicked successfully!");
        }

        public async Task LeaveSession() {
            if (ActiveSession == null) return;
            UnregisterSessionEvents();

            try {
                await ActiveSession.LeaveAsync();
                Debug.Log($"Session {ActiveSession.Id} left successfully!");
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }
            finally {
                ActiveSession = null;
            }
        }

        public async Task<IList<ISessionInfo>> QuerySessions() {
            var sessionQueryOptions = new QuerySessionsOptions();
            var results = await MultiplayerService.Instance.QuerySessionsAsync(sessionQueryOptions);
            return results.Sessions;
        }

        public async Task SetPlayerName(string name) {
            if (!PlayerNameRequirementsMet(name)) {
                Debug.LogError("Player name needs to be min 2 characters");
                return;
            }

            if (playerName == name) {
                Debug.Log("Player name is already set to " + playerName);
                return;
            }

            playerName = name;
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        }
        
        public static bool PlayerNameRequirementsMet(string playerName) {
            return !string.IsNullOrEmpty(playerName) && !string.IsNullOrWhiteSpace(playerName) && playerName.Length >= 2;
        }
        
        public static bool SessionsNameRequirementsMet(string sessionsName) {
            return !string.IsNullOrEmpty(sessionsName) && !string.IsNullOrWhiteSpace(sessionsName) && sessionsName.Length >= 2;
        }
    }
}