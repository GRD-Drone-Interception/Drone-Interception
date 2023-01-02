using System.Collections.Generic;

namespace Drone
{
    public class DroneSwarm
    {
        public List<InterceptorDrone> Drones => _droneSwarm;
        private List<InterceptorDrone> _droneSwarm = new();

        public void AddToSwarm(InterceptorDrone drone) => _droneSwarm.Add(drone);

        public void AddToSwarm(IEnumerable<InterceptorDrone> drones) => _droneSwarm.AddRange(drones);

        public void RemoveFromSwarm(InterceptorDrone drone) => _droneSwarm.Remove(drone);
    }
}
