using System;
using System.Collections;
using System.Collections.Generic;
// using System.Numerics; // What is this for?
using UnityEngine;

public class Actor : MonoBehaviour
{
    // Protected fields because inherited classes need to access these values

    //[SerializeField] protected float moveSpeed = 7f;
    //[SerializeField] private GameInput gameInput;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    protected bool isWalking;

    private void Update()
    {
        //Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //transform.position += moveDir * moveSpeed * Time.deltaTime;

        //isWalking = moveDir != Vector3.zero;


        //Attacking
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        //float rotateSpeed = 12f;
        //transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    /* 
    Trigger "Attack" animation on Player.
    Create a list of "enemy" colliders that
    are in player's attack range. Enemies
    determined by their layermask("Enemy").
    For each enemy in attackEnemies call
    TakeDamage for the enemy.
    */
    public void Attack()
    {
        ActorAnimator actorAnimator = GetComponentInChildren<ActorAnimator>();
        if (actorAnimator != null)
        {
            actorAnimator.AttackAnimation();
        }
        Collider[] attackEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in attackEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(10); //calls TakeDamage for the enemy with 10hp damage.
        }

    }

    // Dev Function - visualize attack range.
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        Debug.Log("Damaged");
    }
    // moveSpeed getter
    /* public float MoveSpeed()
    {
        return moveSpeed;
    } */
}
