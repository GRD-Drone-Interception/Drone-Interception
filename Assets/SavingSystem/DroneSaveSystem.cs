using System.IO;
using System.Linq;
using DroneLoadout.Scripts;
using UnityEngine;

namespace SavingSystem
{
    public class DroneSaveSystem
    {
        public static void Save(Drone drone)
        {
            // Create a file to save the drone data to
            string filePath = $"{Application.persistentDataPath}/droneData.json";
            Debug.Log($"Path: {filePath}");

            // Assemble the drone data
            DroneData droneData = new DroneData
            {
                droneName = drone.DroneConfigData.DroneName,
                droneDescription = drone.DroneConfigData.DroneDescription,
                droneType = drone.DroneConfigData.DroneType,
                cost = drone.DecorableDrone.Cost,
                range = drone.DecorableDrone.Range,
                topSpeed = drone.DecorableDrone.TopSpeed,
                acceleration = drone.DecorableDrone.Acceleration,
                weight = drone.DecorableDrone.Weight,
                mountedAttachmentPointIndex = drone.GetAttachmentPoints().FindAll(ap => ap.HasAttachment).Select(ap => drone.GetAttachmentPoints().IndexOf(ap)).ToArray(),
                attachmentDataPaths = drone.MountedAttachmentPointsDictionary().Values.Select(attachment => attachment.Data.PrefabDataPath).ToArray(),
                numAttachments = drone.NumOfMountedAttachments,
                attachmentTypes = drone.MountedAttachmentPointsDictionary().Values.Select(attachment => attachment.Data.AttachmentType).ToArray()
            };

            // Convert the drone data to JSON format
            string json = JsonUtility.ToJson(droneData, true);

            // Write the JSON data to the file
            File.WriteAllText(filePath, json);
        }

        public static DroneData Load()
        {
            var filePath = $"{Application.persistentDataPath}/droneData.json";
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                return JsonUtility.FromJson<DroneData>(jsonData);
            }

            Debug.LogError("File not found at " + filePath);
            return null;
        }
    }
}