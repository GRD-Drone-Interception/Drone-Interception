using DroneLoadout.Scripts;
using UnityEngine;
using Utility;

namespace Testing
{
    /// <summary>
    /// Loads the drone data from a JSON file and instantiates the corresponding drone prefab with saved attachments and decal color.
    /// </summary>
    public abstract class DroneLoader
    {
        public static void Assemble(Drone drone)
        {
            // Load corresponding drone data file
            DroneData droneData = JsonFileHandler.Load<DroneData>(drone.DroneConfigData.DroneName);
        
            // Instantiate attachment prefabs and position them at their attachment point positions specified in the droneData file 
            int i = 0;
            foreach (var attachmentDictionary in droneData.attachmentDictionaries)
            {
                var path = droneData.attachmentDataPaths[i];
            
                GameObject droneAttachmentPrefab = Resources.Load<GameObject>(path);
                GameObject spawnedDroneAttachment = Object.Instantiate(droneAttachmentPrefab);
                DroneAttachment droneAttachment = spawnedDroneAttachment.GetComponent<DroneAttachment>();
                drone.Decorate(droneAttachment, drone.GetAttachmentPoints()[attachmentDictionary.attachmentPointIndex]);
                i++;
            }

            drone.Paint(droneData.decalColour);
        }
    }
}
