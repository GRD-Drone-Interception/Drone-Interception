using DroneLoadout.Scripts;
using SavingSystem;
using UnityEngine;

public abstract class DroneSavedAttachmentsAssembler
{
    public static void BuildDrone(Drone drone)
    {
        if (!DroneSaveSystem.CheckFileExists(drone))
        {
            Debug.Log($"{drone} has no saved sava");
            return;
        }
        
        // Load corresponding drone data file
        DroneData droneData = DroneSaveSystem.Load(drone.DroneConfigData.DroneName);
        
        // Instantiate attachment prefabs and position them at their attachment point positions specified in the droneData file 
        for (var i = 0; i < droneData.numAttachments; i++)
        {
            var path = droneData.attachmentDataPaths[i];
            
            GameObject droneAttachmentPrefab = Resources.Load<GameObject>(path);
            GameObject spawnedDroneAttachment = Object.Instantiate(droneAttachmentPrefab);
            DroneAttachment droneAttachment = spawnedDroneAttachment.GetComponent<DroneAttachment>();
            drone.Decorate(droneAttachment, drone.GetAttachmentPoints()[droneData.mountedAttachmentPointIndex[i]]);
        }
            
        drone.Paint(droneData.decalColour);
    }
}
