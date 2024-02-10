using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorAnimator : MonoBehaviour
{
    [SerializeField] private Actor actor;

    private const string IS_WALKING = "IsWalking";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, actor.IsWalking());
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, actor.IsWalking());
    }
}
