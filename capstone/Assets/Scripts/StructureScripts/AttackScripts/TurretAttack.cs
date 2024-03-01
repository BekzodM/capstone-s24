using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : Attack
{
    public TurretAttack(string name, int damage) : base(name, damage)
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(gameObject);
        }
    }
}
