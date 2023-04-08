using System.IO;
using UnityEngine;

namespace Utility
{
    /// <summary>
    /// Provides static methods for saving, loading, and deleting data in JSON format from a file.
    /// </summary>
    public abstract class JsonFileHandler
    {
        public static void Save(SaveData saveData, string dataPath)
        {
            // Create a file to save the drone data to
            string filePath = $"{Application.persistentDataPath}/{dataPath}.json";
            Debug.Log($"Path: {filePath}");

            // Convert the drone data to JSON format
            string json = JsonUtility.ToJson(saveData, true);

            // Write the JSON data to the file
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Provides functionality for loading 'SaveData' of a specified type from a JSON file.
        /// </summary>
        /// <typeparam name="T">The type of save data to load, which must be derived from the SaveData abstract class.</typeparam>
        /// <param name="dataPath">The name of the JSON file containing the save data to load.</param>
        /// <returns>The deserialized save data of the specified type if the file exists; otherwise, null.</returns>
        public static T Load<T>(string dataPath) where T : SaveData
        {
            var filePath = $"{Application.persistentDataPath}/{dataPath}.json";
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<T>(jsonData);
            }

            Debug.LogError("File not found at " + filePath);
            return null;
        }

        public static void Delete(string dataPath)
        {
            var filePath = $"{Application.persistentDataPath}/{dataPath}.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                Debug.LogError("File not found at " + filePath);
            }
        }

        public static bool CheckFileExists(string fileName)
        {
            var filePath = $"{Application.persistentDataPath}/{fileName}.json";
            return File.Exists(filePath);
        }
    }
}