using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Support : Structure
{
    protected List<GameObject> alliesInZone;
    [SerializeField] protected float cooldown = 1f;
    private float nextCooldown = 0f;
    private bool isSupporting = false;

    protected Support(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base(name, description, "Support", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Start()
    {
        base.Start();
        alliesInZone= new List<GameObject>();
    }
    protected void Update()
    {
        if (isSupporting && Time.time >= nextCooldown) {
            if (alliesInZone.Count > 0) {
                if (planningPhaseManager.activeSelf)
                {
                    Debug.Log("Cannot heal because planning phase is active");
                }
                else {
                    SupportAbility();
                    PlayAudio();
                }

            }
            nextCooldown= Time.time + cooldown;
        }
    }

    public virtual void StartSupport(GameObject other) {
        Debug.Log("Add");
        alliesInZone.Add(other);
        isSupporting= true;
        nextCooldown= Time.time;
    }

    public virtual void EndSupport(GameObject other) {
        Debug.Log("Remove");
        alliesInZone.Remove(other);
        if (alliesInZone.Count ==  0) {
            isSupporting= false;
        }
    }

    protected virtual void SupportAbility() {}

}
