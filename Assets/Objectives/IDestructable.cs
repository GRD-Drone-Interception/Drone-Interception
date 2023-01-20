using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestructable
{
    float MaxHealth { get; }
    float CurrentHealth { get; }

    void Damage(float damage);
    void Destory();
}
