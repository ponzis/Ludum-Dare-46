using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : InteractveController
{

    [SerializeField]
    public InteractveController triggered;

    private Animator animator;

    private bool isDown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    protected override void PlayerInRange(PlayerController player)
    {
        if (!isDown)
        {
            animator.SetTrigger("Pull Lever");
            triggered?.Trigger(true);
            isDown = true;
        }
    }
}
