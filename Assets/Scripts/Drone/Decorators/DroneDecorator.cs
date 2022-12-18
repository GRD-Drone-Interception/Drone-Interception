using Drone.Component;

namespace Drone.Decorators
{
    public class DroneDecorator : IDrone
    {
        private readonly IDrone _decoratedDrone;
        private readonly DroneAttachmentSO _attachmentSo;

        public DroneDecorator(IDrone drone, DroneAttachmentSO droneAttachmentSo)
        {
            _decoratedDrone = drone;
            _attachmentSo = droneAttachmentSo;
        }

        public float Range => _decoratedDrone.Range + _attachmentSo.Range;
        public float Speed => _decoratedDrone.Speed + _attachmentSo.Speed;
        public float Acceleration => _decoratedDrone.Acceleration + _attachmentSo.Acceleration;
        public float Weight => _decoratedDrone.Weight + _attachmentSo.Weight;
    }
}