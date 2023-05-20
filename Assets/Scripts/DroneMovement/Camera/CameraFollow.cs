using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public string attackerTag = "Attacker";

    private GameObject[] attackerObjects;
    private int currentTargetIndex = 0;
    private Transform _currentTarget;

    void Start()
    {
        // Find all objects with the "Attacker" tag
        attackerObjects = GameObject.FindGameObjectsWithTag(attackerTag);

        // Set the initial target
        if (attackerObjects.Length > 0)
        {
            _currentTarget = attackerObjects[currentTargetIndex].transform;
        }
    }

    void LateUpdate()
    {
        // If the current target is null or invisible, acquire a new target
        if (_currentTarget == null || !IsVisible(_currentTarget.gameObject))
        {
            AcquireNewTarget();
        }

        // Check if all attackers are invisible
        bool allAttackersInvisible = CheckAllAttackersInvisible();

        // If all attackers are invisible, set target to null
        if (allAttackersInvisible)
        {
            _currentTarget = null;
        }

        // Update the camera position based on the current target
        if (_currentTarget != null)
        {
            transform.position = new Vector3(_currentTarget.position.x, _currentTarget.position.y + 175, _currentTarget.position.z);
        }
    }

    void AcquireNewTarget()
    {
        // Check if there are any remaining attackers
        if (currentTargetIndex < attackerObjects.Length - 1)
        {
            currentTargetIndex++;
        }
        else
        {
            // Reset to the first attacker if all have been checked
            currentTargetIndex = 0;
        }

        _currentTarget = attackerObjects[currentTargetIndex].transform;
    }

    bool IsVisible(GameObject target)
    {
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }

        return false;
    }

    bool CheckAllAttackersInvisible()
    {
        foreach (GameObject attacker in attackerObjects)
        {
            if (IsVisible(attacker))
            {
                return false;
            }
        }

        return true;
    }
    
    public void FindAndAddAllAttackerDrones()
    {
        attackerObjects = GameObject.FindGameObjectsWithTag(attackerTag);
        AcquireNewTarget();
    }
}