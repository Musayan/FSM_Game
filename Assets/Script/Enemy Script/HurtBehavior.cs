using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBehavior : StateMachineBehaviour
{
    private EnemyFunction enemy;
    private float _timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyFunction>();
        _timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _timer += Time.deltaTime;
        if (_timer >= enemy._hurtAnim.length)
        {
            ChooseState(animator);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void ChooseState(Animator animator)
    {
        if (enemy.CheckIfDead())
            animator.SetTrigger("Dead");
        else if (enemy.CheckPlayer())
            animator.SetTrigger("Chase");
        else if (!enemy.CheckPlayer())
            enemy.Rotate();
        else if (enemy.AttackDistance())
            animator.SetTrigger("Attack");
    }

}
