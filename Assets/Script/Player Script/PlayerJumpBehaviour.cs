using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerJumpBehaviour : StateMachineBehaviour
{
    private PlayerControl player;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<PlayerControl>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player.isGrounded())
        {
            if (player._inputMove.x != 0)
                animator.SetTrigger("Run");
            else if (player._inputMove.x == 0)
                animator.SetTrigger("Idle");
        }
        else if (player._rb.velocity.y < 0)
        {
            animator.SetTrigger("GoingDown");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

}
