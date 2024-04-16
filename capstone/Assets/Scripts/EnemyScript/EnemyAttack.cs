using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float attackRange = 4f;
    public string playerTag = "Player";
    public string structureTag = "Structure";

    float attackCooldown = 1f;
    float lastAttackTime = -9999f;

    void OnTriggerStay(Collider other) {
        if(other.CompareTag(playerTag) || other.CompareTag(structureTag)) {
            Debug.Log("Player entered trigger zone");
            if(Time.time - lastAttackTime >= attackCooldown) {
                if(other.CompareTag(playerTag)) {
                    other.GetComponent<Player>().TakeDamage(10);
                }
                if(other.CompareTag(structureTag)) {
                    Turret turret = other.GetComponentInParent<Turret>();
                    if (turret != null)
                    {
                        turret.TakeDamage(10);
                    }
                    else
                    {
                        Debug.LogWarning("Turret component not found on object tagged as 'Structure'");
                    }
                }
                lastAttackTime = Time.time;
            }
        }
    }
}
