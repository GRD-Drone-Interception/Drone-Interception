namespace Drone
{
    public interface IDrone
    {
        public float Range { get; }
        public float Speed { get; }
        public float Acceleration { get; }
        public float Weight { get; }
    }
}
