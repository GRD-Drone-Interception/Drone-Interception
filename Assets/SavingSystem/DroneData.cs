using System;
using System.Collections.Generic;
using DroneLoadout.Factory;
using DroneLoadout.Scripts;
using UnityEngine;

namespace SavingSystem
{
    [Serializable]
    public class DroneData
    {
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