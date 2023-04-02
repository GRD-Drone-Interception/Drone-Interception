using DroneLoadout;
using UnityEngine;

public abstract class DroneBehaviour : ScriptableObject
{
    public abstract void UpdateBehaviour(Drone drone);
    public abstract void FixedUpdateBehaviour(Drone drone);
}