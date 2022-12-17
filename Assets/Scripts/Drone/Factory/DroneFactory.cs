using Drone.Component;
using Drone.Concrete;
using Drone.Decorators;

namespace Drone
{
    public static class DroneFactory
    {
        public static IDrone CreateDrone(DroneType droneType, DroneConfig droneConfig)
        {
            IDrone drone = null;

            switch (droneType)
            {
                case DroneType.Quadcopter:
                    drone = new QuadcopterDrone(droneConfig);
                    break;
                case DroneType.FixedWing:
                    drone = new FixedWingDrone(droneConfig);
                    break;
            }
            
            return drone;
        }
    }
}

public enum DroneType // Type Object pattern? Use scriptable objects to allow users to define their own types?
{
    Quadcopter,
    FixedWing
}