using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Offensive : Structure
{
    [SerializeField] private GameObject attackPrefab;
    protected Offensive(string name, string description, int cost, int health, float areaEffectRadius)
        : base(name, description, "Offensive", cost, health, areaEffectRadius)
    {
    }

    public void Shoot(GameObject target, float projectileDistanceFromHead = 1f) {
        GameObject model = transform.GetChild(1).gameObject;
        GameObject modelHead = model.transform.GetChild(0).gameObject;
        Vector3 direction = target.transform.position - modelHead.transform.position;
        if (direction != Vector3.zero)
        {
            //rotate head
            modelHead.transform.rotation = Quaternion.LookRotation(direction);
            //instantiate projectile
            Vector3 spawnPos = modelHead.transform.position + modelHead.transform.forward * projectileDistanceFromHead;
            Instantiate(attackPrefab, spawnPos, Quaternion.identity);
        }
    }
}
