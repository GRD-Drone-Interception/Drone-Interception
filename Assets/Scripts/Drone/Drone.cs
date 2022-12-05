namespace Drone
{
    public class Drone : IDrone
    {
        private readonly DroneConfig _droneConfig;
    
        public Drone(DroneConfig droneConfig)
        {
            _droneConfig = droneConfig;
        }

        public float Range => _droneConfig.Range;
        public float Speed => _droneConfig.Speed;
        public float Acceleration => _droneConfig.Acceleration;
        public float Weight => _droneConfig.Weight;
    }
}
