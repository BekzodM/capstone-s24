using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wall : Defensive
{
    [SerializeField] private float scaleSize;
    protected bool canDamage = false;
    protected int wallDamage = 0;
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
        scaleSize = 1f;
        //SetWallScaleSize(scaleSize);
    }

    private void SetWallScaleSize(float size) {
        transform.GetChild(1).localScale = new Vector3(size, size, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canDamage) {
            if (collision.gameObject.CompareTag("Enemy")) {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(wallDamage);
            }
        }
    }

    //upgrades
    //slot0
    protected override void Slot0UpgradeLevel1()
    {
        health += 100;
    }

    protected override void Slot0UpgradeLevel2()
    {
        health += 100;
    }

    protected override void Slot0UpgradeLevel3()
    {
        health += 100;
    }

    protected override void Slot0UpgradeLevel4()
    {
        health += 100;
    }

    protected override void Slot0UpgradeLevel5()
    {
        health += 100;
    }
    //slot1
    protected override void Slot1UpgradeLevel1()
    {
        scaleSize = scaleSize * 0.5f + scaleSize;
        SetWallScaleSize(scaleSize);
    }

    protected override void Slot1UpgradeLevel2()
    {
        scaleSize = scaleSize * 0.5f + scaleSize;
        SetWallScaleSize(scaleSize);
    }

    protected override void Slot1UpgradeLevel3()
    {
        scaleSize = scaleSize * 0.5f + scaleSize;
        SetWallScaleSize(scaleSize);
    }

    protected override void Slot1UpgradeLevel4()
    {
        scaleSize = scaleSize * 0.5f + scaleSize;
        SetWallScaleSize(scaleSize);
    }

    protected override void Slot1UpgradeLevel5()
    {
        scaleSize = scaleSize * 0.5f + scaleSize;
        SetWallScaleSize(scaleSize);
    }
    //slot2
    protected override void Slot2UpgradeLevel1()
    {
        canDamage = true;
        wallDamage = 5;
    }

    protected override void Slot2UpgradeLevel2()
    {
        canDamage = true;
        wallDamage = 10;
    }

    protected override void Slot2UpgradeLevel3()
    {
        canDamage = true;
        wallDamage = 15;
    }

    protected override void Slot2UpgradeLevel4()
    {
        canDamage = true;
        wallDamage = 20;
    }

    protected override void Slot2UpgradeLevel5()
    {
        canDamage = true;
        wallDamage = 25;
    }

}
