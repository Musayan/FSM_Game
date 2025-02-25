using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackBehaviour : StateMachineBehaviour
{
    private BossFunction bossFun;
    private AnimationClip animationClip;
    private float duration;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossFun = animator.GetComponent<BossFunction>();
        animationClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
        duration = 0;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        duration += Time.deltaTime;

        if (duration >= animationClip.length)
        {
            if (bossFun.PlayerInRange())
            {
                animator.SetTrigger("Idle");
            }
            else if (!bossFun.PlayerInRange())
            {
                animator.SetTrigger("Walk");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossFun._attackDelay = bossFun._setDelay;
        animator.ResetTrigger("Attack");
    }

}
