using System;
using DroneLoadout.Factory;
using DroneLoadout.Scripts;
using UnityEngine;

namespace SavingSystem
{
    [Serializable]
    public class DroneData
    {
        public int[] mountedAttachmentPointIndex;
        public int numAttachments;
        public string[] attachmentDataPaths;
        public DroneAttachmentType[] attachmentTypes;
        public Color decalColour;
    }
}