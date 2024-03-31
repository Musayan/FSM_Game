using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour
{
    private EnemyFunction enemy;
    private bool _chasePlayer;
    private float _waitNext;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyFunction>();
     
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy._attackCoolDown -= Time.deltaTime;
        _waitNext -= Time.deltaTime;

        CheckCollisions();

        if (_chasePlayer)
            enemy.ChargePlayer();
        else if (!_chasePlayer)
            animator.SetTrigger("Idle");


            checkDistance(animator);
        
    }

    private void checkDistance(Animator animator)
    {
       
            if (enemy.AttackDistance())
            {
            if (enemy._attackCoolDown < 0)
                animator.SetTrigger("Attack");
            else if (enemy._attackCoolDown > 0)
                animator.SetTrigger("AttackDelay");
            }
        
    }
    private void CheckCollisions()
    {
        if(enemy.CheckPlayer() && !enemy.CheckObstacles())
        {
            _chasePlayer = true;
        }
        else
        {
            _chasePlayer = false;
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Chase");
    }

}
