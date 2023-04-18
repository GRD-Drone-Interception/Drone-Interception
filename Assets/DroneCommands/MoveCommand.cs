using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneCommands
{
    public class MoveCommand : ICommand
    {
        private Drone _drone;
        private Vector3 _destination;
        private Vector3 _previousDestination;
        
        public MoveCommand(Drone drone, Vector3 destination)
        {
            _drone = drone;
            _destination = destination;
        }
        
        public void Execute()
        {
            _previousDestination = _drone.transform.position;
            _drone.Move(_destination);
        }

        public void Undo()
        {
            _drone.Move(_previousDestination);
        }
    }
}
