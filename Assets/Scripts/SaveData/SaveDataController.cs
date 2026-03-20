using System;
using System.IO;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    [SerializeField] private string _fileName = string.Empty;

    private void Awake()
    {
       
    }

    private void SaveData()
    {
        string nameToUse = string.IsNullOrWhiteSpace(_fileName) ? "autoSaved.json" : _fileName;
        if (!nameToUse.EndsWith(".json")) nameToUse += ".json";

        // 1. Intentar cargar la puntuación existente antes de guardar
        int existingHighScore = GetExistingPoints(nameToUse);

        // 2. Comprobar si la puntuación actual es mayor
        if (Main.Player.Points > existingHighScore)
        {
            PlayerData data = new PlayerData();
            data.Points = Main.Player.Points;
            data.TimeSaveData = DateTime.Now.ToString();

            string dataJson = JsonUtility.ToJson(data);
            SaveJsonFile(dataJson, nameToUse);

            Debug.Log($"¡Nuevo High Score! Guardado: {data.Points} puntos.");
        }
        else
        {
            Debug.Log($"Puntuación actual ({Main.Player.Points}) no supera el récord ({existingHighScore}). No se guarda.");
        }
    }

    private int GetExistingPoints(string filename)
    {
        string filePath = Path.Combine(Application.persistentDataPath, filename);

        if (File.Exists(filePath))
        {
            try
            {
                string content = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(content))
                {
                    PlayerData existingData = JsonUtility.FromJson<PlayerData>(content);
                    return existingData.Points;
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"No se pudo leer el archivo previo para comparar puntos: {ex.Message}");
            }
        }
        return -1;
    }

    private void SaveJsonFile(string jsonContent, string filename)
    {
        string filePath = Path.Combine(Application.persistentDataPath, filename);

        try
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            File.WriteAllText(filePath, jsonContent);
            Debug.Log($"Archivo actualizado en: {filePath}");
        }
        catch (IOException ex)
        {
            Debug.LogError($"Error al escribir el archivo: {ex.Message}");
        }
    }

    private void OnDestroy()
    {
        
    }
}
