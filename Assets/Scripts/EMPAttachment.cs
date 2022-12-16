using Drone;
using UnityEngine;

public class EMPAttachment : MonoBehaviour
{
    public DroneAttachment Attachment => droneAttachment;
    [SerializeField] private DroneAttachment droneAttachment;
}
