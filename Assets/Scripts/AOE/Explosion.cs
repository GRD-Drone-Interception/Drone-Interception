using DroneLoadout.Scripts;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5f; // The radius of the area of effect
    public LayerMask layerMask; // The layer(s) that the area of effect should affect
    public ParticleSystem particleEffect; // The particle effect to play when the area of effect triggers
    public float explosionForce = 2.5f; // The force of the explosion
    public float upwardsModifier = 1f; // The upwards modifier of the explosion

    private void OnCollisionEnter(Collision other)
    {
        // Check if the collided object is on the specified layer(s) and has either the "Defender" tag or the "Goal" name
        if (/*layerMask == (1 << other.gameObject.layer) && */other.gameObject.CompareTag("Defender")) /*|| other.gameObject.name == "Goal")*/
        {
            // Get all rigidbodies within the area of effect
            Rigidbody[] rigidbodies = Physics.OverlapSphere(transform.position, radius, layerMask, QueryTriggerInteraction.Ignore)
                .Select(c => c.attachedRigidbody)
                .Distinct()
                .ToArray();

            // Loop through all rigidbodies and apply explosion force to them
            foreach (Rigidbody rb in rigidbodies)
            {
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, radius, upwardsModifier, ForceMode.Impulse);
                    
                    Drone drone = rb.GetComponent<Drone>();
                    if (drone != null)
                    {
                        drone.Die();
                    }
                }
            }

            Instantiate(particleEffect, transform.position, Quaternion.identity);
            
            // Play the particle effect
            /*if (particleEffect != null)
            {
                particleEffect.Play();
            }*/

            // Call the Die() function on the object with the Explosion script attached
            Drone droneOnThisObject = GetComponent<Drone>();
            if (droneOnThisObject != null)
            {
                droneOnThisObject.Die();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to represent the area of effect
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
