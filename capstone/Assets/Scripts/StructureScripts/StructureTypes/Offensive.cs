using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Offensive : Structure
{
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private float firingRate;
    [SerializeField] private float projectileDistanceFromHead = 1.5f;
    [SerializeField] private float projectileSpeed = 20f;
    protected Offensive(string name, string description, int cost, int health, float areaEffectRadius)
        : base(name, description, "Offensive", cost, health, areaEffectRadius)
    {
    }

    public void Shoot(GameObject target) {
        GameObject model = transform.GetChild(1).gameObject;
        Transform modelHead = model.transform.GetChild(0);
        Vector3 direction = target.transform.position - modelHead.transform.position;
        if (direction != Vector3.zero)
        {
            //rotate head
            modelHead.rotation = Quaternion.LookRotation(direction);

            //instantiate projectile
            Vector3 spawnPos = modelHead.position + modelHead.forward * projectileDistanceFromHead;
            if (attackPrefab != null) {
                GameObject projectileInstance = Instantiate(attackPrefab, spawnPos, Quaternion.identity);
                
                //launching projectile
                Rigidbody attackRB = projectileInstance.GetComponent<Rigidbody>();
                Debug.Log(attackRB.name);
                if (attackRB != null)
                {
                    attackRB.velocity = direction.normalized * projectileSpeed;            
                }
            }


        }
    }
}
