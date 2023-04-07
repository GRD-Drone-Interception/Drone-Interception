using System.Collections.Generic;
using System.IO;
using DroneLoadout.Scripts;
using UnityEngine;

namespace SavingSystem
{
    public abstract class DroneSaveSystem
    {
        public static void Save(Drone drone)
        {
            // Create a file to save the drone data to
            string filePath = $"{Application.persistentDataPath}/{drone.DroneConfigData.DroneName}.json";
            Debug.Log($"Path: {filePath}");

            // Assemble the drone data
            DroneData droneData = new DroneData();
            droneData.droneCost = drone.DecorableDrone.Cost;
            droneData.droneType = droneData.droneType;
            droneData.numAttachments = drone.GetAttachmentPoints().Count;
            droneData.attachmentDictionaries = new List<AttachmentDictionary>();
            droneData.attachmentDataPaths = new List<string>();
            droneData.decalColour = drone.GetPaintJob();
            
            int i = 0;
            foreach (var mountedAttachmentIndex in drone.GetAttachmentPointTypeIndex().Keys)
            {
                AttachmentDictionary attachmentDictionary = new AttachmentDictionary();
                attachmentDictionary.attachmentPointIndex = mountedAttachmentIndex;
                attachmentDictionary.attachmentType = drone.GetAttachmentPointTypeIndex()[mountedAttachmentIndex];
                droneData.attachmentDictionaries.Add(attachmentDictionary);
                droneData.attachmentDataPaths.Add(drone.GetAttachmentPoints()[mountedAttachmentIndex].GetDroneAttachment().Data.PrefabDataPath);
                i++;
            }
            
            // Convert the drone data to JSON format
            string json = JsonUtility.ToJson(droneData, true);

            // Write the JSON data to the file
            File.WriteAllText(filePath, json);
        }

        public static DroneData Load(string dataPath)
        {
            var filePath = $"{Application.persistentDataPath}/{dataPath}.json";
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<DroneData>(jsonData);
            }

            Debug.LogError("File not found at " + filePath);
            return null;
        }

        public static void Delete(Drone drone)
        {
            var filePath = $"{Application.persistentDataPath}/{drone.DroneConfigData.DroneName}.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            else
            {
                Debug.LogError("File not found at " + filePath);
            }
        }

        public static bool CheckFileExists(string droneName)
        {
            var filePath = $"{Application.persistentDataPath}/{droneName}.json";
            return File.Exists(filePath);
        }
    }
}