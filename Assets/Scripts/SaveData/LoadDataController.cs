using System.IO;
using UnityEngine;

public class LoadDataController : MonoBehaviour
{
    [SerializeField] private string _fileName = string.Empty;

    private void Start()
    {
        LoadData();
    }
    private void LoadData()
    {
        string nameToUse = string.IsNullOrWhiteSpace(this._fileName) ? "autoSaved.json" : this._fileName;

        // Asegurar extensión
        if (!nameToUse.EndsWith(".json")) nameToUse += ".json";

        string content = LoadJsonFromFile(nameToUse);

        if (string.IsNullOrEmpty(content))
        {
            Debug.Log("No se encontró el archivo o está vacío: " + nameToUse);
            return;
        }

        PlayerData data = JsonUtility.FromJson<PlayerData>(content);
        Debug.Log("Información cargada");
        Main.CustomEvents.OnDataLoaded?.Invoke(data);
        Debug.Log($"Información cargada con éxito. Puntos: {data.Points} | Fecha: {data.TimeSaveData}");
    }

    private string LoadJsonFromFile(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(filePath))
        {
            try
            {
                string content = File.ReadAllText(filePath);
                return content;
            }
            catch (IOException ex)
            {
                Debug.LogError($"Error al leer el archivo: {ex.Message}");
            }
        }

        return string.Empty;
    }
}
