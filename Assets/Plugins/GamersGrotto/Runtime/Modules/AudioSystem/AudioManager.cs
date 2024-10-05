using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioSource uiSoundPlayer;
    public AudioSource musicPlayer;

    private short uiSoundIndex;

    #region UI

    public void PlayUISound(AudioClip clip) {
        uiSoundPlayer.clip = clip;
        uiSoundPlayer.time = 0;
        uiSoundPlayer.Play();
    }

    /// <summary>
    /// This method plays the sound from the AudioCollectionSO in order, looping when it reaches the end of the collection
    /// Optional way to play UI sounds, see Crusader Kings 2 country selection sounds for an example
    /// </summary>
    /// <param name="audioCollection"></param>
    public void PlayUISound(AudioCollectionSO audioCollection) {
        if (audioCollection.AudioClips.Length == 0) {
            Debug.LogWarning("AudioCollection is empty");
            return;
        }

        PlayUISound(audioCollection.AudioClips[uiSoundIndex]);
        uiSoundIndex = (short)((uiSoundIndex + 1) % audioCollection.AudioClips.Length);
    }

    public void PlayRandomUISound(AudioCollectionSO audioCollection) {
        PlayUISound(audioCollection.GetRandomClip());
    }

    #endregion

    #region Music

    public void PlayMusic(AudioClip clip) {
        musicPlayer.clip = clip;
        musicPlayer.loop = true;
        musicPlayer.time = 0;
        musicPlayer.Play();
    }

    /// <summary>
    /// Plays the music from the AudioCollectionSO in order
    /// Loops when it reaches the end of the collection
    /// </summary>
    /// <param name="audioCollection"></param>
    public void PlayMusic(AudioCollectionSO audioCollection) {
        StopCoroutine(nameof(PlayMusicRoutine));
        if (audioCollection.AudioClips.Length == 0) {
            Debug.LogWarning("AudioCollection is empty");
            return;
        }

        StartCoroutine(PlayMusicRoutine(audioCollection));
    }

    private IEnumerator PlayMusicRoutine(AudioCollectionSO audioCollection) {
        while (true) {
            foreach (var clip in audioCollection.AudioClips) {
                PlayMusic(clip);
                yield return new WaitForSeconds(clip.length);
            }
        }
    }

    #endregion
}