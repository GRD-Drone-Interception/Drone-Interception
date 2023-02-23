using DroneSetup.Component;
using DroneSetup.Decorators;

namespace DroneSetup.Concrete
{
    public class QuadcopterDrone : IDrone 
    {
        private readonly DroneConfigSO _droneConfigSo;
    
        public QuadcopterDrone(DroneConfigSO droneConfigSo)
        {
            _droneConfigSo = droneConfigSo;
        }

        public float Cost => _droneConfigSo.Cost;
        public float Range => _droneConfigSo.Range;
        public float Speed => _droneConfigSo.Speed;
        public float Acceleration => _droneConfigSo.Acceleration;
        public float Weight => _droneConfigSo.Weight;
    }
}
