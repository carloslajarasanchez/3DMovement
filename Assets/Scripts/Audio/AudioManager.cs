using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager
{
    private GameObject _parentPooling;
    private AudioSource _musicSource;
    private List<AudioSource> _audioSources = new List<AudioSource>();

    public AudioManager(GameObject parent)
    {
        this._parentPooling = parent;
        _musicSource = _parentPooling.AddComponent<AudioSource>();
        _musicSource.loop = true;
    }

    /// <summary>
    /// Reproduce un AudioClip pasado por parámetro
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(AudioClip clip)
    {
        var audioSource = this.GetOrCreateAudioSource();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlaySound(string audioKey)
    {
        AudioClip clip = Main.AudioDatabase.GetClip(audioKey);
        if (clip != null)
        {
            PlaySound(clip); // Llama a tu método original que ya maneja el pooling
        }
    }

    // Nueva sobrecarga para música
    public void PlayMusic(string musicKey)
    {
        AudioClip clip = Main.AudioDatabase.GetClip(musicKey);
        if (clip != null) PlayMusic(clip);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void ResumeMusic()
    {
        _musicSource.UnPause();
    }
    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    /// <summary>
    /// Devuelve o crea un AudioSource Idle
    /// </summary>
    /// <returns></returns>
    private AudioSource GetOrCreateAudioSource()
    {
        // Buscamos de la lista de AudioSources el primero Idle
        AudioSource audioSource = this._audioSources.Where(x => x.isPlaying == false).FirstOrDefault();

        if (audioSource == null)
        {
            audioSource = this._parentPooling.AddComponent<AudioSource>();
            this._audioSources.Add(audioSource);
        }

        return audioSource;
    }
}
