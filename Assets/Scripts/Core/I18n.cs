using System;
using System.Collections.Generic;
using UnityEngine;

public class I18n
{
    // Diccionario para hacer busqueda rapida de las traducciones
    Dictionary<string, string> _translationsDictionary = new Dictionary<string, string>();

    //Constructor
    public I18n(string language = "es")
    {
        this.ChangeLanguage(language);
    }

    public void ChangeLanguage(string language)
    {
        TextAsset contentJson = Resources.Load<TextAsset>($"i18n/{language}");

        //Si el archivo no existe
        if (contentJson == null)
        {
            Debug.Log($"[i18n] No se encontro el archivo de idioma: Resources/i18n/{language}");
            return;
        }

        try
        {
            // Deserializamos el JSON al objeto de transferencia
            TranslationsDTO translationsDTO = JsonUtility.FromJson<TranslationsDTO>(contentJson.text);

            if (translationsDTO != null && translationsDTO.translations != null)
            {
                // Convertimos la lista del JSON a diccionario para optimizar el acceso
                this._translationsDictionary = this.ConvertToDictionary(translationsDTO.translations);

                // Lanzamos evento de cambio de lenguage
                Main.CustomEvents.OnLanguageChanged?.Invoke();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"[i18n] Error al parsear el JSON de idioma: {e.Message}");
        }
    }

    private Dictionary<string, string> ConvertToDictionary(List<TranslateItem> translations)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();

        foreach (var translation in translations)
        {
            if (!result.TryAdd(translation.key, translation.value))
            {
                Debug.LogWarning($"[i18n] Clave duplicada detectada y omitida: {translation.key}");
            }
        }
        return result;
    }

    public string Get(string key)
    {
        if (string.IsNullOrEmpty(key)) return string.Empty;

        if (!_translationsDictionary.TryGetValue(key, out string value))
        {
            return value;
        }

        return value;
    }
}
