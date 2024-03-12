using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Wall : Defensive
{
    public Wall(int cost, int health)
        : base("Wall", "A basic wall", cost, health)
    {
    }

    protected override void Start()
    {
        base.Start();
        SetStructureName("Wall");
        SetDescription("The basic defensive structure.");
    }

}
