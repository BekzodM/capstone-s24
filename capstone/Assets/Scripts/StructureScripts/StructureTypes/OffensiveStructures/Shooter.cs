using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Offensive
{
    protected float attackSpeed;
    protected float attackDamage;
    protected float critChance;
    protected float critDamage;
    protected Attack projectile;
    public Shooter(int cost, int health, float areaEffectRadius, float attackSpeed, float attackDamage, float critChance, float critDamage, Attack projectile)
        : base("Shooter", "Structures that shoot projectiles", cost, health, areaEffectRadius)
    {
        this.attackSpeed = attackSpeed;
        this.attackDamage = attackDamage;
        this.critChance = critChance;
        this.critDamage = critDamage;
        this.projectile = projectile;
    }
}
