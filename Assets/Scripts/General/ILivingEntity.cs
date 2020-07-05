using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Interface for all living entities
public interface ILivingEntity
{
    float health {get; set;}
    void TakeDamage(float damage);
}
