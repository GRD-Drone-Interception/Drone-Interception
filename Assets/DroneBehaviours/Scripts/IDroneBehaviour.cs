using DroneLoadout;
using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    public abstract class DroneBehaviour : ScriptableObject
    {
        public abstract void UpdateBehaviour(Drone drone);
        public abstract void FixedUpdateBehaviour(Drone drone);
    }
}