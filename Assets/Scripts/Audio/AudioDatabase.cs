using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioDatabase", menuName = "Sounds/AudioDatabase")]
public class AudioDatabase : ScriptableObject
{
    public List<AudioData> audioList;

    private Dictionary<string, AudioClip> _audioDictionary;

    public void Initialize()
    {
        _audioDictionary = new Dictionary<string, AudioClip>();
        foreach (var data in audioList)
        {
            if (!_audioDictionary.ContainsKey(data.key))
            {
                _audioDictionary.Add(data.key, data.clip);
            }
        }
    }

    public AudioClip GetClip(string key)
    {
        if (_audioDictionary != null && _audioDictionary.TryGetValue(key, out AudioClip clip))
        {
            return clip;
        }

        Debug.LogWarning($"El sonido {key} no existe en la base de datos.");
        return null;
    }
}
