using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class IdleBehaviour : StateMachineBehaviour
{
    private EnemyFunction enemy;    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       enemy = animator.GetComponent<EnemyFunction>();  
       enemy._rbEny.velocity = Vector3.zero;    
       enemy._waitNextPoint = 3f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy._waitNextPoint -= Time.deltaTime;

        if (enemy._waitNextPoint < 0)
        {
            animator.SetTrigger("Patrol");
        }

        if (enemy.CheckPlayer() && !enemy.CheckObstacles())
        {
            animator.SetTrigger("Chase");
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }


}
