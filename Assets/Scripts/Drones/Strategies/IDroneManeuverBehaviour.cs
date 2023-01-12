namespace Drones.Strategies
{
    /// <summary>
    /// Interface that each drone maneuver strategy should inherit from
    /// </summary>
    public interface IDroneManeuverBehaviour
    {
        void Maneuver(Drone drone);
    }
}
