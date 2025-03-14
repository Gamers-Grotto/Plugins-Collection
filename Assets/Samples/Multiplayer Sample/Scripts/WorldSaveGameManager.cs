using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamersGrotto.Multiplayer_Sample {
    public class WorldSaveGameManager : MonoBehaviour {
        public static WorldSaveGameManager Instance { get; private set; }
        [SerializeField] int worldSceneIndex = 1;

        private void Awake() {
            if (Instance != null) {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public async Task LoadNewGame() {
            await SceneManager.LoadSceneAsync(worldSceneIndex);
        }
    }
}