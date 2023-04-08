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
        public float droneCost;
        public DroneType droneType;
        public int numAttachments;
        public List<AttachmentDictionary> attachmentDictionaries;
        public List<string> attachmentDataPaths;
        public Color decalColour;
    }
    
    [Serializable]
    public class AttachmentDictionary
    {
        public int attachmentPointIndex;
        public DroneAttachmentType attachmentType;
    }
}