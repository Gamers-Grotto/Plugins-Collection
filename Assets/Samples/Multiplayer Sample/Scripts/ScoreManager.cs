using System;
using System.Collections.Generic;
using System.Text;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Multiplayer_Sample
{
    public class ScoreManager : NetworkBehaviour
    {
        private NetworkList<ScoreData> Scores = new ();
        public UnityEvent<List<ScoreData>> OnScoresUpdated;

        public List<ScoreData> ScoreList => Scores.AsList();

        public static ScoreManager Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void AddScore(ulong attackerClientId, int points) {
            Instance.AddScoreServerRpc(attackerClientId, points);
        }
        
        [ServerRpc]
        private void AddScoreServerRpc(ulong attackerClientId, int points)
        {
            AddScoreInternal(attackerClientId, points);
        }
        
        void AddScoreInternal(ulong attackerClientId, int points)
        {
            if(!IsHost)
                return;
            
            for (var i = 0; i < Scores.Count; i++)
            {
                if (Scores[i].PlayerId == attackerClientId)
                {
                    var newValue = Scores[i];
                    newValue.Score += points;
                    Scores[i] = newValue;
                    return;
                }
            }
            
            Scores.Add(new ScoreData()
            {
                PlayerId = attackerClientId,
                Score = points
            });
        }

        public void ResetScores()
        {
            Scores.Clear();
        }


        private void OnEnable()
        {
            Scores.OnListChanged += OnListChanged;
        }

        private void OnDisable()
        {
            Scores.OnListChanged -= OnListChanged;
        }

        private void OnListChanged(NetworkListEvent<ScoreData> changeevent)
        {
            OnScoresUpdated?.Invoke(Scores.AsList());
        }

        [ContextMenu("Log Scores")]
        public void Test()
        {
            var scores = Scores.AsList();
            Debug.Log("NetworkScores length : " +Scores.Count);
            Debug.Log($"Scores Length = {scores.Count}");

            var sb = new StringBuilder();
            foreach (var score in scores)
            {
                sb.AppendLine($"Player {score.PlayerId} : score : {score.Score}");
            }
            Debug.Log(sb);
        }
    }
    [GenerateSerializationForType(typeof(ScoreData))]
    public struct ScoreData : IEquatable<ScoreData>, INetworkSerializeByMemcpy
    {
        public ulong PlayerId;
        public int Score;

        public bool Equals(ScoreData other)
        {
            return PlayerId == other.PlayerId && Score == other.Score;
        }

        public override bool Equals(object obj)
        {
            return obj is ScoreData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PlayerId, Score);
        }
    }
}