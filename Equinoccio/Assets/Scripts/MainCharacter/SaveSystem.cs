using System.IO;
using UnityEngine;

public static class SaveSystem
{
    // Ruta guardado permanente
    private static string savePath = Application.persistentDataPath + "/save.json";

    // Ruta guardado temporal entre escenas
    private static string tempSavePath = Application.persistentDataPath + "/tempSave.json";

    // Guarda en archivo permanente
    public static void SaveGame(CharData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Juego guardado permanentemente en: " + savePath);
    }

    // Carga desde archivo permanente
    public static CharData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            CharData data = JsonUtility.FromJson<CharData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado permanente.");
            return null;
        }
    }

    // Guarda temporalmente entre escenas (se puede sobrescribir varias veces)
    public static void SaveTemp(CharData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(tempSavePath, json);
        Debug.Log("Guardado temporal entre escenas en: " + tempSavePath);
    }

    // Carga guardado temporal
    public static CharData LoadTemp()
    {
        if (File.Exists(tempSavePath))
        {
            string json = File.ReadAllText(tempSavePath);
            CharData data = JsonUtility.FromJson<CharData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado temporal.");
            return null;
        }
    }

    // Elimina archivo permanente
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Archivo de guardado permanente eliminado.");
        }
    }

    // Elimina archivo temporal
    public static void DeleteTempSave()
    {
        if (File.Exists(tempSavePath))
        {
            File.Delete(tempSavePath);
            Debug.Log("Archivo de guardado temporal eliminado.");
        }
    }
}