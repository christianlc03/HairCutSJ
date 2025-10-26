using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Se mantiene aunque recargues la escena
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
        }
        else
        {
            Destroy(gameObject); // evitar duplicados
        }
    }

    public void PlayMusic(AudioClip clip, float fadeTime = 1f)
    {
        if (clip == null) return;

        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            // ya está sonando la misma música
            return;
        }

        StopAllCoroutines();
        StartCoroutine(FadeMusic(clip, fadeTime));
    }

    private System.Collections.IEnumerator FadeMusic(AudioClip newClip, float fadeTime)
    {
        float startVolume = audioSource.volume;

        // Fade out
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeTime);
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        for (float t = 0; t < fadeTime; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, startVolume, t / fadeTime);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume; // valor entre 0 y 1
    }
    public float GetVolume()
    {
        return audioSource.volume;
    }
}
