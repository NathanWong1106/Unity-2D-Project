using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if the player has entered the attack trigger, then call the Attack function of Enemy
        if (collision.gameObject.name.Equals("Player"))
        {
            enemy.Attack(collision.GetComponent<Player>());
        }
    }
}
