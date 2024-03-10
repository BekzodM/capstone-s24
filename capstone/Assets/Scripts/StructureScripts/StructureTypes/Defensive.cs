using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Defensive : Structure
{
    protected Defensive(string name, string description, int cost, int health)
        : base(name, description, "Defensive", cost, health)
    {
    }
}
