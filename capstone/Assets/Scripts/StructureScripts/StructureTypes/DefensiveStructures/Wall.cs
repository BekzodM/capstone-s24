using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wall : Defensive
{
    public ParticleSystem flameParticles;
    public ParticleSystem embersParticles;
    public Wall(string name, string description, int cost, int health, int progressLevel, int attackDamage)
        : base("Wall", "A basic wall", cost, health, progressLevel, attackDamage)
    {
    }

    protected override void Awake()
    { 
        SetStructureName("Wall");
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
    }

    private void SetWallScaleSize(float sizeScaleFactor) {
        Transform wall = transform.GetChild(1);
        float x = wall.localScale.x;
        float y = wall.localScale.y;
        float z = wall.localScale.z;
        wall.localScale = new Vector3(x * sizeScaleFactor + x, y * sizeScaleFactor + y, z * sizeScaleFactor + z);
    }

    public override void StartDefensiveAttack(GameObject other) { 
        base.StartDefensiveAttack(other);
        //Debug.Log("Start Defensive Wall Attack");
    }

    public override void EndDefensiveAttack(GameObject other)
    {
        base.EndDefensiveAttack(other);
        //Debug.Log("End Defensive Wall Attack");
    }

    //upgrades
    //slot0
    protected override void Slot0UpgradeLevel1()
    {
        maxHealth += 100;
        health += 100;
    }

    protected override void Slot0UpgradeLevel2()
    {
        maxHealth += 100;
        health += 100;
    }

    protected override void Slot0UpgradeLevel3()
    {
        maxHealth += 100;
        health += 100;
    }

    protected override void Slot0UpgradeLevel4()
    {
        maxHealth += 100;
        health += 100;
    }

    protected override void Slot0UpgradeLevel5()
    {
        maxHealth += 100;
        health += 100;
    }
    //slot1
    protected override void Slot1UpgradeLevel1()
    {
        SetWallScaleSize(0.2f);
    }

    protected override void Slot1UpgradeLevel2()
    {
        SetWallScaleSize(0.2f);
    }

    protected override void Slot1UpgradeLevel3()
    {
        SetWallScaleSize(0.2f);
    }

    protected override void Slot1UpgradeLevel4()
    {
        SetWallScaleSize(0.2f);
    }

    protected override void Slot1UpgradeLevel5()
    {
        SetWallScaleSize(0.2f);
    }
    //slot2
    protected override void Slot2UpgradeLevel1()
    {
        areaZone.SetActive(true);
        flameParticles.Play();
        embersParticles.Play();
        attackDamage = 5;
        cooldown= 10;
    }

    protected override void Slot2UpgradeLevel2()
    {
        attackDamage = 10;
        cooldown= 10;
    }

    protected override void Slot2UpgradeLevel3()
    {
        attackDamage = 15;
        cooldown= 10;
    }

    protected override void Slot2UpgradeLevel4()
    {
        attackDamage = 20;
        cooldown= 10;
    }

    protected override void Slot2UpgradeLevel5()
    {
        attackDamage = 25;
        cooldown= 10;
    }

}
