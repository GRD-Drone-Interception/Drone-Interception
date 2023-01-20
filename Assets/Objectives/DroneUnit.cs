using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DroneUnit : MonoBehaviour
{
    [SerializeField] private PlayerTeam team;
    public PlayerTeam Team => team;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
