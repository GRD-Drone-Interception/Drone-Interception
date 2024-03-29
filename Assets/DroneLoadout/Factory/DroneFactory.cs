﻿using System;
using DroneLoadout.Concrete;
using DroneLoadout.Decorators;
using DroneLoadout.Scripts;

namespace DroneLoadout.Factory
{
    public static class DroneFactory
    {
        /// <summary>
        /// Creates a drone of a given type and configuration
        /// </summary>
        /// <param name="droneType">The concrete drone class i.e. Quadcopter</param>
        /// <param name="droneConfigData">The default drone configuration properties</param>
        /// <returns></returns>
        public static IDrone CreateDrone(DroneType droneType, DroneConfigData droneConfigData)
        {
            IDrone drone = null;

            switch (droneType)
            {
                case DroneType.Quadcopter:
                    drone = new QuadcopterDrone(droneConfigData);
                    break;
                case DroneType.FixedWing:
                    drone = new FixedWingDrone(droneConfigData);
                    break;
                case DroneType.Interceptor:
                    drone = new InterceptorDrone(droneConfigData);
                    break;
            }
            
            return drone;
        }
    }

    public enum DroneType 
    {
        Quadcopter,
        FixedWing,
        Interceptor
    }
}