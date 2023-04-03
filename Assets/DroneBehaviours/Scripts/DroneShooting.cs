using DroneLoadout;
using UnityEngine;

namespace DroneBehaviours.Scripts
{
    [CreateAssetMenu(fileName = "NewDroneShootingBehaviour", menuName = "Drone/Behaviours/Shooting")]
    public class DroneShooting : DroneBehaviour
    {
        public override void UpdateBehaviour(Drone drone)
        {
            //Debug.Log("Shooting behaviour", drone);
        }

        public override void FixedUpdateBehaviour(Drone drone)
        {
        
        }
    }
}
