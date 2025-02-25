using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtBehaviour : StateMachineBehaviour
{
    private BossFunction bossFun;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossFun = animator.GetComponent<BossFunction>();
        if (bossFun.bossDead())
        {
            bossFun.OnDisableBoss();
            animator.SetTrigger("Dead");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bossFun.bossDead())
        {
            bossFun.OnDisableBoss();
            bossFun._powerUpStart = false;  
            animator.SetTrigger("Dead");
        }
        else if (!bossFun.bossDead())
        {
            animator.SetTrigger("Idle");
        }
       

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Hurt");
    }

}
