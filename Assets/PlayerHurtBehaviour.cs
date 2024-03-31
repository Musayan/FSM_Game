using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHurtBehaviour : StateMachineBehaviour
{
    private PlayerControl player;
    private float hurtDuration;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerControl>();
        hurtDuration = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hurtDuration += Time.deltaTime;

        if (player._hurtAnim.length < hurtDuration)
        {
            CheckIfDead(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      
   }
    private void CheckIfDead(Animator animator)
    {
        if (player.CheckPlayerIsDead())
            animator.SetTrigger("Dead");
        else
            animator.SetTrigger("Idle");
    }


}
