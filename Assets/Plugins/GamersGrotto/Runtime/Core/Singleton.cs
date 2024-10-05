using UnityEngine;

namespace GamersGrotto.Runtime.Core
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        [Header("Singleton settings")] 
        [SerializeField] private bool dontDestroyOnLoad = true;
        public static T Instance { get; private set; }
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            
            if(dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}
