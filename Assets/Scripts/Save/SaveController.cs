using System.IO;
using UnityEngine;

public class SaveController
{
    public static void SaveObject<T>(T obj, string path) where T : SaveObject
    {
        obj.Serialize();

        string jsonString = JsonUtility.ToJson(obj);

        File.WriteAllText(path, jsonString);
    }

    public static T LoadObject<T>(string path) where T : SaveObject
    {
        T obj = default;

        if (File.Exists(path))
        {
            string fileContents = File.ReadAllText(path);

            obj = JsonUtility.FromJson<T>(fileContents);
        }

        obj.Deserialize();

        return obj;
    }

    public static T LoadObject<T>(string path, out bool fileExists) where T : SaveObject
    {
        T obj = default;

        fileExists = false;

        if (File.Exists(path))
        {
            string fileContents = File.ReadAllText(path);

            obj = JsonUtility.FromJson<T>(fileContents);

            fileExists = true;
        }

        if (obj != null)
        {
            obj.Deserialize();
        }

        return obj;
    }
}