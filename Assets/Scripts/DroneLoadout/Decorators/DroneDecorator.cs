using DroneLoadout.Component;

namespace DroneLoadout.Decorators
{
    /// <summary>
    /// Used for decorating drones with attachments
    /// </summary>
    public class DroneDecorator : IDrone
    {
        private readonly IDrone _decoratedDrone;
        private readonly DroneAttachmentSO _attachmentSo;

        /// <summary>
        /// Decorates a given drone by modifying it's attributes based on the newly supplied attachment.
        /// </summary>
        /// <param name="drone">The decorable drone</param>
        /// <param name="droneAttachmentSo">The default attribute values for a drone attachment</param>
        public DroneDecorator(IDrone drone, DroneAttachmentSO droneAttachmentSo)
        {
            _decoratedDrone = drone;
            _attachmentSo = droneAttachmentSo;
        }

        public float Cost => _decoratedDrone.Cost + _attachmentSo.Cost;
        public float Range => _decoratedDrone.Range + _attachmentSo.Range;
        public float Speed => _decoratedDrone.Speed + _attachmentSo.Speed;
        public float Acceleration => _decoratedDrone.Acceleration + _attachmentSo.Acceleration;
        public float Weight => _decoratedDrone.Weight + _attachmentSo.Weight;
    }
}