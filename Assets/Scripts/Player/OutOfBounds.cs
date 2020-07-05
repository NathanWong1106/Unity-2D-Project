using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ILivingEntity other = collision.GetComponent<ILivingEntity>();

        if(other != null)
        {
            other.TakeDamage(other.health);
        }
    }
}
