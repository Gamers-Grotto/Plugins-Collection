using GamersGrotto.Core.Extended_Attributes;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.MultiplayerSample {
    public class TitleScreenManager : MonoBehaviour {
        [Button]
        public void StartNetworkAsHost() {
            NetworkManager.Singleton.StartHost();
        }

        [Button]
        public void StartNewGame() {
            StartCoroutine(WorldSaveGameManager.Instance.LoadNewGame());
        }
    }
}