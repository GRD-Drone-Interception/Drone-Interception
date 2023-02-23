using DroneSetup.Component;
using DroneSetup.Concrete;
using DroneSetup.Decorators;

namespace DroneSetup.Factory
{
    public static class DroneFactory
    {
        /// <summary>
        /// Creates a drone of a given type and configuration
        /// </summary>
        /// <param name="droneType">The concrete drone class i.e. Quadcopter</param>
        /// <param name="droneConfigSo">The default drone configuration properties</param>
        /// <returns></returns>
        public static IDrone CreateDrone(DroneType droneType, DroneConfigSO droneConfigSo)
        {
            IDrone drone = null;

            switch (droneType)
            {
                case DroneType.Quadcopter:
                    drone = new QuadcopterDrone(droneConfigSo);
                    break;
                case DroneType.FixedWing:
                    drone = new FixedWingDrone(droneConfigSo);
                    break;
            }
            
            return drone;
        }
    }

    public enum DroneType // Type Object pattern? Use scriptable objects to allow users to define their own types?
    {
        Quadcopter,
        FixedWing
    }
}