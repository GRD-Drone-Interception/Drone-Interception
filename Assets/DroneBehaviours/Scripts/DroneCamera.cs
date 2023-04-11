using DroneLoadout.Scripts;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    [CreateAssetMenu(fileName = "NewDroneCameraBehaviour", menuName = "Drone/Behaviours/Camera")]
    public class DroneCamera : DroneBehaviour
    {
        public override void UpdateBehaviour(Drone drone)
        {
            //Debug.Log("Camera behaviour", drone);
        }

        public override void FixedUpdateBehaviour(Drone drone)
        {
        
        }
    }
}