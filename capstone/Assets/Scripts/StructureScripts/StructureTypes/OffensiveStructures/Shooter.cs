using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : Offensive
{
    protected float attackDamage;
    protected float critChance;
    protected float critDamage;
    public Shooter(int cost, int health, float areaEffectRadius, float attackSpeed, float attackDamage, float critChance, float critDamage, Attack projectile)
        : base("Shooter", "Structures that shoot projectiles", cost, health, areaEffectRadius)
    {
        this.attackDamage = attackDamage;
        this.critChance = critChance;
        this.critDamage = critDamage;
    }
}
