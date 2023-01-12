using System.Collections.Generic;

namespace Drones
{
    public class DroneSwarm
    {
        public List<Drone> Drones => _droneSwarm;
        private List<Drone> _droneSwarm = new();

        public void AddToSwarm(Drone drone) => _droneSwarm.Add(drone);

        public void AddToSwarm(IEnumerable<Drone> drones) => _droneSwarm.AddRange(drones);

        public void RemoveFromSwarm(Drone drone) => _droneSwarm.Remove(drone);
    }
}
