using Drone.Component;
using Drone.Decorators;

namespace Drone.Concrete
{
    public class FixedWingDrone : IDrone 
    {
        private readonly DroneConfig _droneConfig;
    
        public FixedWingDrone(DroneConfig droneConfig)
        {
            _droneConfig = droneConfig;
        }

        public float Range => _droneConfig.Range;
        public float Speed => _droneConfig.Speed;
        public float Acceleration => _droneConfig.Acceleration;
        public float Weight => _droneConfig.Weight;
    }
}