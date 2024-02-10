using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected string attackName;
    protected int damage;
    protected Attack(string name, int damage) {
        attackName = name;
        this.damage = damage;
    }
}
