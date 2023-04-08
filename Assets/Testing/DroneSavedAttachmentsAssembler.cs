using DroneLoadout.Scripts;
using SavingSystem;
using UnityEngine;

public abstract class DroneSavedAttachmentsAssembler
{
    public static void BuildDrone(Drone drone)
    {
        if (!DroneSaveSystem.CheckFileExists(drone.DroneConfigData.DroneName))
        {
            Debug.Log($"{drone} has no saved sava");
            return;
        }
        
        // Load corresponding drone data file
        DroneData droneData = DroneSaveSystem.Load(drone.DroneConfigData.DroneName);
        
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
