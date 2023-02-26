using DroneLoadout.Component;

namespace DroneLoadout.Decorators
{
    /// <summary>
    /// Used for decorating drones with attachments.
    /// </summary>
    public class DroneDecorator : IDrone
    {
        private readonly IDrone _decoratedDrone;
        private readonly DroneAttachmentData _attachmentData;

        /// <summary>
        /// Decorates a given drone by modifying it's attributes based on the newly supplied attachment.
        /// </summary>
        /// <param name="drone">The decorable drone</param>
        /// <param name="droneAttachmentData">The default attribute values for a drone attachment.</param>
        public DroneDecorator(IDrone drone, DroneAttachmentData droneAttachmentData)
        {
            _decoratedDrone = drone;
            _attachmentData = droneAttachmentData;
        }

        public float Cost => _decoratedDrone.Cost + _attachmentData.Cost;
        public float Range => _decoratedDrone.Range + _attachmentData.Range;
        public float Speed => _decoratedDrone.Speed + _attachmentData.Speed;
        public float Acceleration => _decoratedDrone.Acceleration + _attachmentData.Acceleration;
        public float Weight => _decoratedDrone.Weight + _attachmentData.Weight;
    }
}