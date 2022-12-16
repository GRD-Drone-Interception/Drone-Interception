using System.Collections.Generic;
using UnityEngine;

namespace Drone
{
    public class FixedWingDrone : MonoBehaviour
    {
        public DroneConfig droneConfig; // SO data
        public DroneAttachment primaryAttachment; // SO data
        public DroneAttachment secondaryAttachment; // SO data

        [SerializeField] private List<AttachmentPoint> attachmentPoints;
        private IDrone _drone;

        private void Start()
        {
            _drone = new Drone(droneConfig);
        }

        [ContextMenu("Decorate with first attachment")]
        public void DecorateOne()
        {
            _drone = new DroneDecorator(_drone, primaryAttachment);
            var attachment = Instantiate(primaryAttachment.attachmentPrefab, attachmentPoints[0].transform.position, Quaternion.identity);
            attachment.transform.SetParent(attachmentPoints[0].transform);
        }
        
        [ContextMenu("Decorate with second attachment")]
        public void DecorateTwo()
        {
            _drone = new DroneDecorator(_drone, secondaryAttachment);
            var attachment = Instantiate(secondaryAttachment.attachmentPrefab, attachmentPoints[1].transform.position, Quaternion.identity);
            attachment.transform.SetParent(attachmentPoints[1].transform);
        }
        
        public void DecorateCustom(GameObject attachment, AttachmentPoint point, DroneAttachment droneAttachment)
        {
            _drone = new DroneDecorator(_drone, droneAttachment);
            //var attachment = Instantiate(secondaryAttachment.attachmentPrefab, attachmentPoints[1].transform.position, Quaternion.identity);
            attachment.transform.position = point.transform.position;
            attachment.transform.SetParent(attachmentPoints[1].transform);
        }

        [ContextMenu("ResetConfig")]
        public void ResetConfiguration()
        {
            _drone = new Drone(droneConfig);
            foreach (var point in attachmentPoints)
            {
                Destroy(point.transform.GetChild(0).gameObject);
            }
        }

        private void Update()
        {
            print($"Drone: {_drone}, Range: {_drone.Range}");
        }
    }
}