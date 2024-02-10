using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    public Turret(int cost, int health, float areaEffectRadius, float attackSpeed, float attackDamage, float critChance, float critDamage, Attack projectile)
        : base(cost, health, areaEffectRadius, attackSpeed, attackDamage, critChance, critDamage, projectile) {
        structureName = "Turret";
    }
}
