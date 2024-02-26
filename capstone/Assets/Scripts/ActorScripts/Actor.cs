using System.Collections;
using System.Collections.Generic;
// using System.Numerics; // What is this for?
using UnityEngine;

public class Actor : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private bool isWalking;
    private bool attack;

    void Start() {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 12f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        //Attacking
        if(Input.GetMouseButtonDown(0)) {
            Attack();
        } 
    }

    public bool IsWalking()
    {
        return isWalking;
    }


    public void Attack(){
        ActorAnimator actorAnimator = GetComponentInChildren<ActorAnimator>();
        if (actorAnimator != null)
        {
            actorAnimator.AttackAnimation();
        }
    }

    void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
