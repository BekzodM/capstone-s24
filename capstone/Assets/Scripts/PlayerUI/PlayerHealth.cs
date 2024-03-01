using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Health = " + health.ToString());
    }
}
