using System;
using DroneLoadout.Factory;
using DroneLoadout.Scripts;

namespace SavingSystem
{
    [Serializable]
    public class DroneData
    {
        public string droneName;
        public string droneDescription;
        public DroneType droneType;
        public float cost;
        public float range;
        public float topSpeed;
        public float acceleration;
        public float weight;
        public int[] mountedAttachmentPointIndex;
        public int numAttachments;
        public string[] attachmentDataPaths;
        public DroneAttachmentType[] attachmentTypes;
    }
}