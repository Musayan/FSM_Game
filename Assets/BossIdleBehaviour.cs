using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleBehaviour : StateMachineBehaviour
{
    private BossFunction bossFun;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossFun = animator.GetComponent<BossFunction>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        bossFun.OnEnableBoss();

        if (bossFun.PlayerInRange() && bossFun.CanAttack())
        {
            //attack state
            animator.SetTrigger("Attack");
        }
        else if (!bossFun.PlayerInRange())
        {
            //Walk state
            animator.SetTrigger("Walk");
        }
        else if (bossFun.PlayerInRange() && !bossFun.CanAttack())
        {
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");  
    }

}
