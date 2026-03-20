using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Main
{
    public static I18n I18n { get; private set; }
    public static CustomEvents CustomEvents { get; private set; }
    public static CustomConfiguration CustomConfiguration { get; private set; }
    public static Player Player { get; private set; }
    public static AudioManager AudioManager { get; private set; }
    public static AudioDatabase AudioDatabase { get; private set; }


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Start()
    {
        CustomEvents = new CustomEvents();
        I18n = new I18n();
        CustomConfiguration = new CustomConfiguration();
        Player = new Player();

        AudioDatabase = Resources.Load<AudioDatabase>("ScriptableObject/AudioDatabase");
        if (AudioDatabase != null)
        {
            AudioDatabase.Initialize();
        }
        SetupAudio();
    }


    private static void SetupAudio()
    {
        // 1. Creamos un objeto persistente para los sonidos
        GameObject audioHost = new GameObject("AudioManager");
        Object.DontDestroyOnLoad(audioHost);

        // 3. Instanciamos el manager
        AudioManager = new AudioManager(audioHost);
    }
}
