using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string playerTag = "Player";
    public string structureTag = "Structure";

    float attackCooldown = 1f;
    float lastAttackTime = -9999f;

    public void EnemyAttackPlayer() {
        if(Time.time - lastAttackTime >= attackCooldown) {
            GameObject.Find("Female 1").GetComponent<Player>().TakeDamage(10);
            lastAttackTime = Time.time;
        }
    }

    public void EnemyAttackStructure(Collider structureCol) {
        if(Time.time - lastAttackTime >= attackCooldown) {
            structureCol.GetComponentInParent<Structure>().TakeDamage(10);
            lastAttackTime = Time.time;
        }
    }
}
