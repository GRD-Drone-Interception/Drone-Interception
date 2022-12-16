using Drone.Component;

namespace Drone.Decorators
{
    public class DroneDecorator : IDrone
    {
        private readonly IDrone _decoratedDrone;
        private readonly DroneAttachment _attachment;

        public DroneDecorator(IDrone drone, DroneAttachment droneAttachment)
        {
            _decoratedDrone = drone;
            _attachment = droneAttachment;
        }

        public float Range => _decoratedDrone.Range + _attachment.Range;
        public float Speed => _decoratedDrone.Speed + _attachment.Speed;
        public float Acceleration => _decoratedDrone.Acceleration + _attachment.Acceleration;
        public float Weight => _decoratedDrone.Weight + _attachment.Weight;
    }
}