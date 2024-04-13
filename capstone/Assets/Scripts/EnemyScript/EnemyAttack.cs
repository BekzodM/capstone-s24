using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    float attackRange = 4f;
    public string playerTag = "Player";
    float attackCooldown = 1f;
    float lastAttackTime = -9999f;

    void OnTriggerStay(Collider other) {
        if(other.CompareTag(playerTag)) {
            Debug.Log("Player entered trigger zone");
            if(Time.time - lastAttackTime >= attackCooldown) {
                Debug.Log("Cooldown passed");
                other.GetComponent<Player>().TakeDamage(10);
                lastAttackTime = Time.time;
            }
        }
    }
}
