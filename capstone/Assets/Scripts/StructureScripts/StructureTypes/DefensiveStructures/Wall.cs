using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Defensive
{
    public Wall(int cost, int health)
        : base("Wall", "A basic wall", cost, health)
    {
    }

    protected override void UseUpgrade0()
    {
    }

    protected override void UseUpgrade1()
    {
    }

    protected override void UseUpgrade2()
    {

    }
}
