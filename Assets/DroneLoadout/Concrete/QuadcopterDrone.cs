using DroneLoadout.Decorators;
using DroneLoadout.Scripts;

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
        public float TopSpeed => _droneConfigData.TopSpeed;
        public float Acceleration => _droneConfigData.Acceleration;
        public float Weight => _droneConfigData.Weight;
    }
}
