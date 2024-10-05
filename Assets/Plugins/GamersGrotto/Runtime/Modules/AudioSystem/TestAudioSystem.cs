using UnityEngine;

public class TestAudioSystem : MonoBehaviour
{
    [SerializeField] private AudioLibrary audioLibrary;
    [SerializeField] private AudioSource audioSource;
    
    [Button]
    public void TestRandomAudioEvent()
    {
        if(!audioLibrary || !audioSource)
            return;
        
        audioLibrary.GetRandomAudioEvent().Play(audioSource, false);
    }
    
}
