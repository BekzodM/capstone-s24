using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    protected string attackName;
    protected int damage;
    private GameObject target;
    protected Attack(string name, int damage, GameObject target) {
        attackName = name;
        this.damage = damage;
        this.target = target;
    }

    private void Start()
    {
        linearProjectilePath();
    }

    private void linearProjectilePath() {
        this.transform.position = target.transform.position;
    }
}
