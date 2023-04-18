using System;
using System.Collections.Generic;
using DroneLoadout.Factory;
using UnityEngine;
using Utility;

namespace DroneLoadout.Scripts
{
    [Serializable]
    public class DroneData : SaveData
    {
        public string droneName;
        public DroneType droneType;
        public float droneCost;
        public int numAttachments;
        public List<AttachmentDictionary> attachmentDictionaries;
        public List<string> attachmentDataPaths;
        public Color decalColour;
        
        public DroneData(Drone drone)
        {
            droneName = drone.GetName();
            droneType = drone.GetDroneType();
            droneCost = drone.DecorableDrone.Cost;
            numAttachments = drone.GetAttachmentPoints().Count;
            attachmentDictionaries = new List<AttachmentDictionary>();
            attachmentDataPaths = new List<string>();
            decalColour = drone.GetPaintJob();
            
            int i = 0;
            foreach (var mountedAttachmentIndex in drone.GetAttachmentPointTypeIndex().Keys)
            {
                AttachmentDictionary attachmentDictionary = new AttachmentDictionary();
                attachmentDictionary.attachmentPointIndex = mountedAttachmentIndex;
                attachmentDictionary.attachmentType = drone.GetAttachmentPointTypeIndex()[mountedAttachmentIndex];
                attachmentDictionaries.Add(attachmentDictionary);
                attachmentDataPaths.Add(drone.GetAttachmentPoints()[mountedAttachmentIndex].GetDroneAttachment().Data.PrefabDataPath);
                i++;
            }
        }
    }
    
    [Serializable]
    public class AttachmentDictionary
    {
        public int attachmentPointIndex;
        public DroneAttachmentType attachmentType;
    }
}