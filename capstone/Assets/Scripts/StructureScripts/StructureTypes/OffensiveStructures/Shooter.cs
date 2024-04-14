using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Offensive
{
    [SerializeField] private float rayDistanceFromHead;
    public GameObject muzzle;
    public GameObject impact;

    public Shooter(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null. Cannot shoot.");
            return;
        }

        GameObject model = transform.GetChild(1).gameObject;
        Transform modelHead = model.transform.GetChild(1);
        Vector3 direction = target.transform.position - modelHead.transform.position;

        if (direction != Vector3.zero)
        {
            //rotate head
            modelHead.rotation = Quaternion.LookRotation(direction);

            //Use Raycast that points to the enemy's direction and damage them
            RaycastHit hit;
            Vector3 startPos = modelHead.position + modelHead.forward * rayDistanceFromHead;
            float distanceToTarget = Vector3.Distance(target.transform.position, startPos);//ray length
            if (Physics.Raycast(startPos, direction, out hit, distanceToTarget))
            {
                Debug.Log(hit.collider.name);
                Debug.DrawLine(startPos, hit.point, Color.blue, 5);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    DealDamage(hit.collider.gameObject);

                    //attacking particles
                    GameObject muzzleInstance = Instantiate(muzzle, startPos, Quaternion.identity);
                    ParticleSystem ps1 = muzzle.GetComponent<ParticleSystem>();

                    if (ps1 != null)
                    {
                        ps1.Play();
                        Destroy(muzzleInstance, ps1.main.duration);
                    }


                    GameObject impactInstance = Instantiate(impact, hit.point, Quaternion.identity);
                    ParticleSystem ps2 = impactInstance.GetComponent<ParticleSystem>();
                    if (ps2 != null)
                    {
                        ps2.Play();
                        Destroy(impactInstance, ps2.main.duration);
                    }

                }
            }
        }
    }


    protected override void DealDamage(GameObject target)
    {
        if (target != null)
        {
            target.GetComponent<Enemy>().TakeDamage(attackDamage, gameObject);
        }
        else
        {
            //Debug.Log("No valid target to deal damage to");
        }

    }

}
