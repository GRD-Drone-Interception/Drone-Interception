using DroneLoadout.Component;
using DroneLoadout.Decorators;

namespace DroneLoadout.Concrete
{
    public class QuadcopterDrone : IDrone 
    {
        private readonly DroneConfigData _droneConfigData;
    
        public QuadcopterDrone(DroneConfigData droneConfigData)
        {
            _droneConfigData = droneConfigData;
        }

        public float Cost => _droneConfigData.Cost;
        public float Range => _droneConfigData.Range;
        public float Speed => _droneConfigData.Speed;
        public float Acceleration => _droneConfigData.Acceleration;
        public float Weight => _droneConfigData.Weight;
    }
}
