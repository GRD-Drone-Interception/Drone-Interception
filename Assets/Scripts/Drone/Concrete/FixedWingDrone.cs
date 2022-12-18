using Drone.Component;
using Drone.Decorators;

namespace Drone.Concrete
{
    public class FixedWingDrone : IDrone 
    {
        private readonly DroneConfigSO _droneConfigSo;
    
        public FixedWingDrone(DroneConfigSO droneConfigSo)
        {
            _droneConfigSo = droneConfigSo;
        }

        public float Range => _droneConfigSo.Range;
        public float Speed => _droneConfigSo.Speed;
        public float Acceleration => _droneConfigSo.Acceleration;
        public float Weight => _droneConfigSo.Weight;
    }
}