using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackDelay : StateMachineBehaviour
{
    private EnemyFunction enemy;
    private float _wait;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<EnemyFunction>();
        _wait = 0.5f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy._attackCoolDown -= Time.deltaTime;
        _wait -= Time.deltaTime;    

        if (enemy._attackCoolDown < 0 && enemy.AttackDistance())
        {
            animator.SetTrigger("Attack");
        }


        if (!enemy.AttackDistance() && _wait < 0)
        {
            animator.SetTrigger("Chase");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("AttackDelay");
    }

   
}
