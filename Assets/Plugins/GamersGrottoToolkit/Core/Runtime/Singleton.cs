using UnityEngine;

namespace GamersGrotto.Core {
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
        [Header("Singleton settings")] [SerializeField]
        private bool dontDestroyOnLoad = true;

        public static T Instance { get; private set; }

        protected virtual void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}