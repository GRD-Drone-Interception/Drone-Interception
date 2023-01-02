using Drone.Component;
using Drone.Decorators;

namespace Drone.Concrete
{
    public class QuadcopterDrone : IDrone 
    {
        private readonly DroneConfigSO _droneConfigSo;
    
        public QuadcopterDrone(DroneConfigSO droneConfigSo)
        {
            _droneConfigSo = droneConfigSo;
        }

        public float Range => _droneConfigSo.Range;
        public float Speed => _droneConfigSo.Speed;
        public float Acceleration => _droneConfigSo.Acceleration;
        public float Weight => _droneConfigSo.Weight;
    }
}
